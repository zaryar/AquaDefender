using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2;
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
