using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : WeaponTemplate
{
    [SerializeField] float lifeTime = 2;
    [SerializeField] int Damage = 1;
    [SerializeField] GameObject Smoke;
    bool _collided = false;

    protected override void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;
        if(collision != null &&
            !_collided &&
           opposingFraction.Contains(collisionTag)) 
        {
            if (collision.gameObject.GetComponent<EnemyTemplate>()!= null)
            {
                collision.gameObject.GetComponent<EnemyTemplate>().Hurt((int)(Damage * (UnityEngine.Random.value > GameController.instance.CritChance ? 1 : 1 + GameController.instance.CritDmg)));
            }
            else if (collision.gameObject.GetComponent<Health>()!= null)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
            }
            else if (collision.gameObject.GetComponent<DragonAI>() != null)
            {
                collision.gameObject.GetComponent<DragonAI>().TakeDamage((int)(Damage * (UnityEngine.Random.value > GameController.instance.CritChance ? 1 : 1 + GameController.instance.CritDmg)));
            }
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Instantiate(Smoke, transform.position, Quaternion.identity);
        }
        _collided = true;
        Destroy(gameObject);
    }
}
