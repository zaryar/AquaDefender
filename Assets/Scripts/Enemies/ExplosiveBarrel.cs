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
    override protected void Die()
    {
        if(!_isDead)
        {
            _isDead= true;
            fire = Instantiate(FireEffect, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
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

                c.gameObject.GetComponent<EnemyTemplate>().Hurt((int)(Damage * (UnityEngine.Random.value > GameController.instance.CritChance ? 1 : 1 + GameController.instance.CritDmg)));
            }
        }
        Destroy(fire);
        base.Die();
    }
}
