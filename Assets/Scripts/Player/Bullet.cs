using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 2;
    [SerializeField] int Damage = 1;
    [SerializeField] GameObject Smoke;
    bool _collided = false;
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null &&
           !_collided &&
           collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<EnemyTemplate>().Hurt(Damage);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Instantiate(Smoke, transform.position, Quaternion.identity);
        }
        _collided = true;
        Destroy(gameObject);
    }
}
