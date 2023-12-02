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
        checkOpposingFraction();
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        tag = collision.gameObject.tag;
        if(collision != null &&
            !_collided &&
           opposingFraction.Contains(tag)) {
            if (collision.gameObject.GetComponent<EnemyTemplate>()!= null)
                {
                collision.gameObject.GetComponent<EnemyTemplate>().Hurt(Damage);
                }
                else if (collision.gameObject.GetComponent<Health>()!= null)
                {
                collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
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
