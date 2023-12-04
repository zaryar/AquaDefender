using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{

    [SerializeField] int maxHealth;
    [SerializeField] int Health;
    [SerializeField] GameObject HitParticle;

    public void Start()
    {
        if (gameObject.name.Contains("Enemy"))
        {
            init_healthbar();
        }
    }

    public void Hurt(int dmg)
    {
        Health -= dmg;
        if(Health <= 0)
        {
            Die();
        }
        else if(gameObject.name.Contains("Enemy"))
        {
            update_healthbar(dmg);
        }
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

    public void adapt_bar(string bar, float substract)
    {
        Transform barobject = gameObject.transform.Find(bar);
        float x_position = barobject.transform.localPosition.x;
        float x_scale = barobject.transform.localScale.x;
        barobject.transform.localPosition = new Vector3(x_position - (0.5f * Math.Abs(substract)), 1.2f, 0);
        barobject.transform.localScale = new Vector3(x_scale + substract, 0.2f, 0.2f);

    }

    public void update_healthbar(int dmg)
    {
        float substract = (float)dmg / (float)maxHealth;
        adapt_bar("HealthBar_Life", -substract);
        adapt_bar("HealthBar_Background", substract);
    }

    public void init_healthbar()
    {
        gameObject.transform.Find("HealthBar_Life").GetComponent<Renderer>().material.color = Color.green;
        gameObject.transform.Find("HealthBar_Background").GetComponent<Renderer>().material.color = Color.red;
    }

}
