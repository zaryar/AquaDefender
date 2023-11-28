using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 2;
    [SerializeField] int Damage = 1;
    bool _collided = false;
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null &&
           !_collided &&
           collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyTemplate>().Hurt(Damage);
        }
        _collided = true;
        Destroy(gameObject);
    }

}
