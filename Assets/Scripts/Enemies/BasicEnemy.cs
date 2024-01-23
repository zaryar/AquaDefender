using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyTemplate
{
    [HideInInspector] public Transform _target;
    [HideInInspector] public Transform _player;
    [HideInInspector] public NavMeshAgent _agent;
    // [SerializeField] float invisibleTime = 5f;
    public event Action OnDeath;
    public event Action OnAttack;
    public GameObject goldPrefab; // Assign the gold prefab in the Inspector window
    public GameObject waterPrefab; // Assign the waterdrop prefab in the Inspector window
    public GameObject barrelCoin; // Assign the BarrelCoin prefab in the Inspector window
    [HideInInspector] public GunTemplate _gun;
    [HideInInspector] public Transform enemy_gun;
    [HideInInspector] public SwordTemplate _sword;
    public AudioClip[] huhClips;

    private bool isFreezed = false;
    private bool isPlayerVisible = true;


    //Helper variables
    int attack_finished = 0;

    public void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar3D>();
        System.Random random = new System.Random();
        Array swords = Enum.GetValues(typeof(Weapons.Swords));
        SwordModelSwapper smw = GetComponentInChildren<SwordModelSwapper>();
        if (smw != null)
        {
            smw.swapModel((Weapons.Swords)swords.GetValue(random.Next(swords.Length)));
            smw.enableRenderer();
        }
    }


    protected override void Die()
    {
        Instantiate(goldPrefab, transform.position, Quaternion.identity);
        float spawnChance = 0.2f; // 20% chance of spawning
        float randomValue = UnityEngine.Random.value; // Generate a random value between 0 and 1

        if (randomValue <= spawnChance)
        {
            Instantiate(waterPrefab, transform.position, Quaternion.identity);
        }
        if (randomValue <= (spawnChance - 0.1f))
        {
            Instantiate(barrelCoin, transform.position + new Vector3(0, 0, 0.3f), Quaternion.identity);
        }
        OnDeath?.Invoke(); // Ereignis auslösen
        _agent.enabled = false;
        base.Die();
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _target = _player;
        _sword = gameObject.transform.Find("Sword").GetComponent<SwordTemplate>();
        enemy_gun = gameObject.transform.Find("Gun");
        _gun = enemy_gun.GetComponent<GunTemplate>();


    }

    public Vector3 Get_sorted_distance(List<Vector3> vectors, Vector3 target, string type)
    {
        Vector3 vec = vectors[0];
        float Distance = Vector3.Distance(vec, target);
        //float Distance = float.MaxValue;
        Vector3 Vector = vectors[0];


        foreach (Vector3 vector in vectors)
        {
            float distance = Vector3.Distance(vector, target);
            if (distance < Distance && type == "min")
            {
                Distance = distance;
                Vector = vector;
            }
            else if (distance > Distance && type == "max")
            {
                Distance = distance;
                Vector = vector;
            }
        }

        return Vector;
    }

    public void orient_player()
    {
        if (_target != null && !isFreezed)
        {
            Vector3 dir = _target.position - transform.position;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
        }
    }

    public void follow_sword_attack()
    {
        //Debug.Log(Vector3.Distance(transform.position, _target.position) + " " + _agent.destination + " " + _target.position);
        if (Vector3.Distance(transform.position, _target.position) >= _sword.GetswordAttackRange() + 1.0f)
        {
            _agent.destination = _target.position;
            attack_finished = 0;
        }
        else if (Vector3.Distance(transform.position, _target.position) <= _sword.GetswordAttackRange() && attack_finished == 0)
        {
            _sword.Attack();
            OnAttack?.Invoke();
            Vector3 direction = _target.position - transform.position;
            direction.Normalize();
            _agent.destination = _target.position - 10 * direction;
            attack_finished = 1;
        }
        else if (transform.position == _agent.destination)
        {
            _agent.destination = _target.position;
            attack_finished = 0;
        }
    }

    public void gun_aim()
    {

        float angle = Vector3.Angle(transform.position - _target.position, Vector3.up);
        float x_rotation = Math.Max(-60f, angle - 90f);
        float z_position = Math.Min(Math.Max(0.2f, 0.2f - (0.002f * (angle - 90f))), 0.35f);
        enemy_gun.transform.localRotation = Quaternion.Euler(x_rotation, 180f, 0f);
        enemy_gun.transform.localPosition = new Vector3(enemy_gun.transform.localPosition.x, enemy_gun.transform.localPosition.y, z_position);
    }

    public void follow_gun_attack()
    {
        Vector3 direction = _target.position - transform.position;
        direction.Normalize();
        _agent.destination = _target.position - 3 * direction;
        gun_aim();
        _gun.Shoot();
    }
    private void Update()
    {
        if (_isDead) return;
        if (_target == _player)
            StartCoroutine(PlayerVisible());


        if (_target != null)
        {
            orient_player();
            follow_sword_attack();
            //follow_gun_attack(); 
        }
    }

    public IEnumerator PlayerVisible()
    {
        if (_player.GetComponent<PlayerMovementController>().invisible)
        {
            _target = null;

            _player.GetComponent<InvisibilityCountdown>().StartCountdown();
            StartCoroutine(huhSounds());
            while (_player.GetComponent<PlayerMovementController>().invisible)
            {
                yield return new WaitForSeconds(0.5f);
            }
            _target = _player;

            _player.GetComponent<InvisibilityCountdown>().StopCountdown();
            _player.GetComponent<InvisibilityCountdown>().StopReload();
        }
    }

    IEnumerator huhSounds()
    {
        if (huhClips.Length <= 0)
            yield break;

        float time = UnityEngine.Random.Range(0f, 2f);
        yield return new WaitForSeconds(time);

        int randomIndex = UnityEngine.Random.Range(0, huhClips.Length);
        AudioSource.PlayClipAtPoint(huhClips[randomIndex], transform.position);

    }

    public IEnumerator freeze(float freezingTime, Material freezingMaterial)
    {
        if (isFreezed)
            yield break;
        isFreezed = true;

        Material[] originalMaterial = new Material[0];
        Renderer enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null && !isFreezed)
        {
            originalMaterial = new Material[enemyRenderer.materials.Length];
            Array.Copy(enemyRenderer.materials, originalMaterial, enemyRenderer.materials.Length);
            Material[] invisibleArr = new Material[enemyRenderer.materials.Length];
            for (int i = 0; i < enemyRenderer.materials.Length; ++i)
                invisibleArr[i] = freezingMaterial;
            enemyRenderer.materials = invisibleArr;
        }

        Renderer[] children = GetComponentsInChildren<Renderer>();
        Material[][] materials = new Material[children.Length][];
        for (int i = 0; i < children.Length; i++)
        {
            int length = children[i].materials.Length;

            materials[i] = new Material[length];
            Array.Copy(children[i].materials, materials[i], length);

            if (children[i].name.StartsWith("Health"))
                continue;

            Material[] invisibleArr = new Material[length];
            for (int j = 0; j < length; ++j)
                invisibleArr[j] = freezingMaterial;
            children[i].materials = invisibleArr;
        }

        float speed = _agent.speed;
        _agent.speed = 0;
        yield return new WaitForSeconds(freezingTime);
        if (_agent == null)
            yield break;

        isFreezed = false;
        _agent.speed = speed;

        if (enemyRenderer != null && originalMaterial.Length > 0)
            enemyRenderer.materials = originalMaterial;

        for (int i = 0; i < children.Length; i++)
        {
            children[i].materials = materials[i];
        }
    }

}
