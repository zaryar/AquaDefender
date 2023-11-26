using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyTemplate
{
    Transform _target;
    NavMeshAgent _agent;
    public GameObject goldPrefab; // Assign the gold prefab in the Inspector window

    protected override void Die()
    {
        Instantiate(goldPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void Awake()
    {
        _agent= GetComponent<NavMeshAgent>();
        _target= GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }
    private void Update()
    {
        _agent.destination = _target.position;
    }
}
