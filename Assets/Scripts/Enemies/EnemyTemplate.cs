using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int Health = 1;
    [SerializeField] GameObject HitParticle;
    public HealthBar3D healthbar;

    public void Hurt(int dmg)
    {
        Health -= dmg;
        Debug.Log(Health);
        if(Health <= 0) Die();
        if(healthbar != null)
        {
            healthbar.update_healthbar(maxHealth, dmg);
        }
    }

    public int getHealth()
    {
        return Health;
    }

    protected virtual void Die()
    {
        //Death Stuff here
        Debug.Log("aua");
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
