using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveBarrel : EnemyTemplate
{
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] float Timer = 2f;
    [SerializeField] int Damage = 1;
    [SerializeField] GameObject FireEffect;
    private GameObject fire;
    private Boolean _isDead = false;
    override protected void Die()
    {
        if(!_isDead)
        {
            _isDead= true;
            fire = Instantiate(FireEffect, transform.position, Quaternion.identity);
            StartCoroutine(CookOff());
        }
    }
    private IEnumerator CookOff()
    {
        yield return new WaitForSeconds(Timer);
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Collider[] HitByExplosion = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (Collider c in HitByExplosion)
        {
            if (c.gameObject.tag == "Enemy" &&
               c.gameObject != gameObject &&
               c.gameObject.GetComponent<EnemyTemplate>().getHealth() > 0)
            {

                c.gameObject.GetComponent<EnemyTemplate>().Hurt(Damage);
            }
        }
        Destroy(fire);
        base.Die();
    }
}
