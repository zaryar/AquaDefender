using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTemplate : MonoBehaviour
{
    Transform attackTransform;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] int Damage = 1;
    [SerializeField] float cooldown = .3f;

    bool _cooldown = false;

    private void Awake()
    {
        attackTransform = GameObject.Find("Muzzle").transform;
       
    }

    public void Attack()
    {
        if (!_cooldown)
        {
            Collider[] hit = Physics.OverlapSphere(attackTransform.position, attackRange);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].gameObject.CompareTag("Enemy"))
                {
                    hit[i].gameObject.GetComponent<EnemyTemplate>().Hurt(Damage);
                }
                
            }

            _cooldown = true;

            StartCoroutine(Cooldown(cooldown));
        }
        
        
    }

    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        _cooldown = false;
    }
}
