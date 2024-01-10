using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SwordTemplate : WeaponTemplate
{
    
    [SerializeField] private float swordAttackRange = 2f;
    [SerializeField] private float swordCooldown = 1.0f;
    [SerializeField] private int swordDamage = 5;
    [SerializeField] private int IceSwordDamage = 1;
    [SerializeField] float freezingTime = 10f;
    [SerializeField] Material freezingMaterial;
    

    public float GetswordAttackRange()
    {
        return swordAttackRange; 
    }
    

    public bool Attack(bool freeze = false)
    {
        if (!onCooldown)
        {
            Collider[] hit = Physics.OverlapSphere(attackTransform.position, attackRange);

            for (int i = 0; i < hit.Length; i++)
            {
                tag = hit[i].tag;
                if (opposingFraction.Contains(tag))
                {
                    if(freeze && hit[i].gameObject.GetComponent<BasicEnemy>()!= null){
                        hit[i].gameObject.GetComponent<EnemyTemplate>().Hurt(IceSwordDamage);
                        StartCoroutine(hit[i].gameObject.GetComponent<BasicEnemy>().freeze(freezingTime, freezingMaterial));
                    }

                    else if(hit[i].gameObject.GetComponent<EnemyTemplate>()!= null)
                    {
                        hit[i].gameObject.GetComponent<EnemyTemplate>().Hurt(swordDamage);
                    }
                    else if(hit[i].gameObject.GetComponent<Health>()!= null)
                    {
                        hit[i].gameObject.GetComponent<Health>().TakeDamage(swordDamage);
                    }

                }
                
            }

            onCooldown = true;

            StartCoroutine(base.Cooldown(cooldown));
            return true;
        }
        return false;
        
    }

    
    

    
}
