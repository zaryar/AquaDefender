using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyTemplate
{
    [SerializeField] GameObject Blood;
    
    Transform _target;
    NavMeshAgent _agent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag == "Bullet")
        {
            Instantiate(Blood, collision.transform.position, Quaternion.identity);
        }
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
