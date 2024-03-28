using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyHumanoidAnimationController : MonoBehaviour
{
    private bool _isRunning = false;
    bool _isDead = false;
    int _isRunningHash;
    Vector3 _moveDir= new Vector3(0f, 0f, 1f);
    int _dirXHash;
    int _dirZHash;
    int _attackHash;
    int _isDeadHash;
    NavMeshAgent _agent;
    [SerializeField] Animator animator;
    [SerializeField] Rig AimRig;

    public void animateSwordAttack()
    {
        animator.SetTrigger(_attackHash);
    }
    public void DeathAnimation()
    {
        _isDead = true;
        animator.SetBool(_isDeadHash, true);
    }

    private void setMoveAnimation()
    {
        animator.SetFloat(_dirXHash, _moveDir.x);
        animator.SetFloat(_dirZHash, _moveDir.z);
    }
    private bool calculateMoveDir()
    {
        float angle = Vector3.Angle(_agent.velocity.normalized, gameObject.transform.forward);
        _moveDir = Quaternion.Euler(0, angle + 180, 0) * new Vector3(0, 0, 1);
        return _agent.velocity.magnitude > 0.2f;
    }
    private void Update()
    {
        if(!_isDead) {
            bool runing = calculateMoveDir();
            if (_isRunning ^ runing)
            {
                _isRunning = runing;
                animator.SetBool(_isRunningHash, runing);
            }
            if(runing)
            {
                setMoveAnimation();
            }
            
        }  
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _isRunningHash = Animator.StringToHash("isRunning");
        _dirXHash = Animator.StringToHash("DirectionX");
        _dirZHash = Animator.StringToHash("DirectionZ");
        _attackHash = Animator.StringToHash("Attack");
        _isDeadHash = Animator.StringToHash("isDead");
    }
    private void Start()
    {
        gameObject.GetComponent<BasicEnemy>().OnDeath += DeathAnimation;
    }
}
