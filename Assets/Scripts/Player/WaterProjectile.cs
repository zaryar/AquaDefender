using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterProjectile : WeaponTemplate
{
    [SerializeField] float lifeTime = 2;
    [SerializeField] int Damage = 1;
    [SerializeField] GameObject WaterDust;
    bool _collided = false;
    protected override void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collision != null &&
           !_collided &&
           opposingFraction.Contains(collisionTag))
        {   
            if (collision.gameObject.GetComponent<EnemyTemplate>()!= null)
            {
            collision.gameObject.GetComponent<EnemyTemplate>().Hurt(Damage);
            }
            else if (collision.gameObject.GetComponent<Health>()!= null)
            {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
            }

        }
        Instantiate(WaterDust, transform.position, Quaternion.identity);
        _collided = true;
        Destroy(gameObject);
    }

}
