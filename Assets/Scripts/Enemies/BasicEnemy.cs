using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyTemplate
{
    Transform _target;
    Transform _player;
    NavMeshAgent _agent;
    // [SerializeField] float invisibleTime = 5f;
    public event Action OnDeath;
    public GameObject goldPrefab; // Assign the gold prefab in the Inspector window
    GunTemplate _gun;
    SwordTemplate _sword;

    //Helper variables
    int attack_finished = 0;

    public void Start()
    {
        healthbar = gameObject.GetComponent<HealthBar3D>();
    }

    protected override void Die()
    {
        Instantiate(goldPrefab, transform.position, Quaternion.identity);
        OnDeath?.Invoke(); // Ereignis auslösen
        Destroy(gameObject);
    }
    private void Awake()
    {
        _agent= GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _target= _player;
        _sword = gameObject.transform.Find("EnemySword").GetComponent<SwordTemplate>();
        _gun = gameObject.transform.Find("EnemyGun").GetComponent<GunTemplate>();
    }

    private void orient_player()
    {
        Vector3 dir = _target.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
    }

    public void follow_sword_attack()
    {
        //Debug.Log(Vector3.Distance(transform.position, _target.position) + " " + _agent.destination + " " + _target.position);
        if (Vector3.Distance(transform.position, _target.position) >= _sword.GetswordAttackRange()+2f)
        {
            _agent.destination = _target.position;
            attack_finished = 0;
        }
        else if (Vector3.Distance(transform.position, _target.position) <= _sword.GetswordAttackRange() && attack_finished==0)
        {
            UnityEngine.Debug.Log("Attack");
            _sword.Attack();
            Vector3 direction = _target.position - transform.position;
            direction.Normalize();
            _agent.destination = _target.position - 10 * direction;
            attack_finished = 1; 
        }
        else if(transform.position == _agent.destination)
        {
            _agent.destination = _target.position;
            attack_finished = 0; 
        }
    }
    private void Update()
    {   
        if(_target == _player)
            StartCoroutine(PlayerVisible());


        if (_target != null)
        {
            orient_player();
            follow_sword_attack();
        }
         
    }

    public IEnumerator PlayerVisible()
    {
        if(_player.GetComponent<PlayerMovementController>().invisible)
        {
            _target=null;

            while(_player.GetComponent<PlayerMovementController>().invisible){
                yield return new WaitForSeconds(0.5f);
            }
            _target = _player;
        }
    }
}
