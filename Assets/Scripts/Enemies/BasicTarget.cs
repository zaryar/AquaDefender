using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTarget : EnemyTemplate
{
    [SerializeField] GameObject Smoke;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag == "Bullet") {
            Instantiate(Smoke, collision.transform.position, Quaternion.identity);
        }
    }
}
