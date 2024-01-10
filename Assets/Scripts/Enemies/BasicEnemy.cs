using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyTemplate
{
    public Transform _target;
    public Transform _player;
    public NavMeshAgent _agent;
    // [SerializeField] float invisibleTime = 5f;
    public event Action OnDeath;
    public GameObject goldPrefab; // Assign the gold prefab in the Inspector window
    public GameObject waterPrefab; // Assign the waterdrop prefab in the Inspector window
    public GunTemplate _gun;
    public SwordTemplate _sword;
    public AudioClip[] huhClips;

    private Renderer enemyRenderer;

    //Helper variables
    int attack_finished = 0;

    public void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar3D>();
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
        OnDeath?.Invoke(); // Ereignis auslösen
        Destroy(gameObject);
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _target = _player;
        _sword = gameObject.transform.Find("Sword").GetComponent<SwordTemplate>();
        _gun = gameObject.transform.Find("Gun").GetComponent<GunTemplate>();

        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer == null)
            enemyRenderer = GetComponentInChildren<Renderer>();
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
        Vector3 dir = _target.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
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
        Transform enemy_gun = gameObject.transform.Find("Gun");
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

            StartCoroutine(huhSounds());
            while (_player.GetComponent<PlayerMovementController>().invisible)
            {
                yield return new WaitForSeconds(0.5f);
            }
            _target = _player;
        }
    }

    IEnumerator huhSounds()
    {
        float time = UnityEngine.Random.Range(0f, 2f);
        yield return new WaitForSeconds(time);

        int randomIndex = UnityEngine.Random.Range(0, huhClips.Length);
        AudioSource.PlayClipAtPoint(huhClips[randomIndex], transform.position);
    }

    public IEnumerator freeze(float freezingTime, Material freezingMaterial)
    {
        Material[] originalArr = new Material[0];
        if (enemyRenderer != null)
        {
            originalArr = new Material[enemyRenderer.materials.Length];
            Array.Copy(enemyRenderer.materials, originalArr, enemyRenderer.materials.Length);
            Material[] invisibleArr = new Material[enemyRenderer.materials.Length];
            for (int i = 0; i < enemyRenderer.materials.Length; ++i)
            {
                invisibleArr[i] = freezingMaterial;
            }
            enemyRenderer.materials = invisibleArr;
        }
        
        _target = null;
        yield return new WaitForSeconds(freezingTime);
        _target = _player;

        if (enemyRenderer != null && originalArr.Length > 0)
            enemyRenderer.materials = originalArr;
    }
}
