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
    }
    private void Update()
    {   
        if(_target == _player)
            StartCoroutine(PlayerVisible());
        

        if(_target != null)
            _agent.destination = _target.position;
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
