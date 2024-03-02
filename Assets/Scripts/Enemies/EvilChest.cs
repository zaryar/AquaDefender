using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine;
using System.Collections;
using TMPro;

public class EvilChest : EnemyTemplate
{
    private Animator animator;
    [HideInInspector] public Transform _player;
    [HideInInspector] public NavMeshAgent _agent;
    [HideInInspector] public Transform _target;
    int damageAmount = 1;
    public float attackRange = 2f;
    public float biteCooldown = 1f;

    private bool isBiting = false;
    private int count = 0;
    private float lastBiteTime;

    void Awake()
    {
        animator = GetComponent<Animator>();
        HealthInit();
        _agent = transform.parent.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _target = _player;
        lastBiteTime = -biteCooldown; // Initialize to allow the first attack immediately
    }

    void Update()
    {
        if (_target== null ||_isDead || !hurt) return;

        //_agent.destination = _player.position;

        if (_target == _player){
            StartCoroutine(PlayerVisible());
            _agent.destination = _player.position;
            if(!isBiting && Vector3.Distance(transform.position, _player.position) <= attackRange)
                StartCoroutine(Bite());
        }
        
    }

    

    IEnumerator Bite() {
        //Debug.Log("Cooldown " + biteCooldown);
        float time = Time.time;
        isBiting = true;
        animator.SetBool("hurt", true);
        _player.GetComponent<Health>().TakeDamage(damageAmount);
        yield return new WaitForSeconds(biteCooldown);
        isBiting = false;
        //Debug.Log(Time.time - time);
        
    }

    public IEnumerator PlayerVisible()
    {
        if (_player.GetComponent<PlayerMovementController>().invisible)
        {
            _target = null;

            _player.GetComponent<InvisibilityCountdown>().StartCountdown();
            
            while (_player.GetComponent<PlayerMovementController>().invisible)
            {
                yield return new WaitForSeconds(0.5f);
            }
            _target = _player;

            _player.GetComponent<InvisibilityCountdown>().StopCountdown();
            _player.GetComponent<InvisibilityCountdown>().StopReload();
        }
    }


}
