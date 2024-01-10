using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SwordTemplate : WeaponTemplate
{
    
    [SerializeField] private float swordAttackRange = 2f;
    [SerializeField] private float swordCooldown = 1.0f;
    [SerializeField] private int swordDamage = 5;
    [SerializeField] float freezingTime = 5f;
    [SerializeField] Material freezingMaterial;
    

    public float GetswordAttackRange()
    {
        return swordAttackRange; 
    }
    
    public void Attack(bool freeze = false)
    {
        if (!onCooldown)
        {
            Collider[] hit = Physics.OverlapSphere(attackTransform.position, attackRange);

            for (int i = 0; i < hit.Length; i++)
            {
                tag = hit[i].tag;
                if (opposingFraction.Contains(tag))
                {
                    if(hit[i].gameObject.GetComponent<EnemyTemplate>()!= null)
                    {
                        hit[i].gameObject.GetComponent<EnemyTemplate>().Hurt(swordDamage);
                    }
                    else if(hit[i].gameObject.GetComponent<Health>()!= null)
                    {
                        hit[i].gameObject.GetComponent<Health>().TakeDamage(swordDamage);
                    }

                    if(freeze && hit[i].gameObject.GetComponent<BasicEnemy>()!= null){
                        StartCoroutine(hit[i].gameObject.GetComponent<BasicEnemy>().freeze(freezingTime, freezingMaterial));
                    }
                }
                
            }

            onCooldown = true;

            StartCoroutine(base.Cooldown(cooldown));
        }
        
        
    }

    // public void IceAttack()
    // {
        
    //         Collider[] hit = Physics.OverlapSphere(attackTransform.position, attackRange);

    //         for (int i = 0; i < hit.Length; i++)
    //         {
    //             tag = hit[i].tag;
    //             if (opposingFraction.Contains(tag) & GetComponent<PlayerMovementController>().freezed)
    //             {
    //                 if(hit[i].gameObject.GetComponent<EnemyTemplate>()!= null)
    //                 {
    //                     hit[i].gameObject.GetComponent<EnemyTemplate>().Hurt(swordDamage);
    //                     StartCoroutine(hit[i].gameObject.GetComponent<BasicEnemy>().freeze());

    //                 }
    //                 else if(hit[i].gameObject.GetComponent<Health>()!= null)
    //                 {
    //                     hit[i].gameObject.GetComponent<Health>().TakeDamage(swordDamage);
    //                 }
    //             }
                
    //         }

        
        
    // }
    

    
}
