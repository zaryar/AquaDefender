using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine;
using System.Collections;

public class EvilChest : EnemyTemplate
{
    private Animator animator;
    [HideInInspector] public Transform _player;
    [HideInInspector] public NavMeshAgent _agent;
    int damageAmount = 1;
    public float attackRange = 2f;
    public float biteCooldown = 0.25f;
    private float lastBiteTime;

    void Awake()
    {
        animator = GetComponent<Animator>();
        HealthInit();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        lastBiteTime = -biteCooldown; // Initialize to allow the first attack immediately
    }

    void Update()
    {
        if (_isDead || !hurt) return;

        _agent.SetDestination(_player.position);

        if (Time.time - lastBiteTime >= biteCooldown && Vector3.Distance(transform.position, _player.position) <= attackRange)
        {   
            Debug.Log("Attacking!");
            Bite();
            lastBiteTime = Time.time;
        }
    }

    public void Bite()
    {
        animator.SetBool("hurt", true);
        _player.GetComponent<Health>().TakeDamage(damageAmount);
    }
}
