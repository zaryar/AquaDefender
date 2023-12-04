using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    [SerializeField] int Health = 1;
    [SerializeField] GameObject HitParticle;
    

    public void Hurt(int dmg)
    {
        Health -= dmg;
        if(Health <= 0) Die();
    }

    public int getHealth()
    {
        return Health;
    }

    protected virtual void Die()
    {
        //Death Stuff here
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null &&
            collision.gameObject.tag == "Bullet" &&
            HitParticle != null)
        {
           
            Instantiate(HitParticle, collision.transform.position, Quaternion.identity);
        }
    }

}
