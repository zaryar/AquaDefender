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
    private bool isFreezed = false;
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

    if(!isFreezed){
        float time = Time.time;
        isBiting = true;
        animator.SetBool("hurt", true);
        _player.GetComponent<Health>().TakeDamage(damageAmount);
        yield return new WaitForSeconds(biteCooldown);
        isBiting = false;
    }
        
        
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

    public IEnumerator freeze(float freezingTime, Material freezingMaterial)
    {
        if (isFreezed)
            yield break;
        isFreezed = true;
        animator.SetBool("freezed", true);

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
        animator.SetBool("freezed", false);
        _agent.speed = speed;

        if (enemyRenderer != null && originalMaterial.Length > 0)
            enemyRenderer.materials = originalMaterial;

        for (int i = 0; i < children.Length; i++)
        {
            children[i].materials = materials[i];
        }
    }



}
