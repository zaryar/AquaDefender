using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    [SerializeField] int Health = 1;

    public void Hurt(int dmg)
    {
        Health -= dmg;
        if(Health <= 0) Die();
    }

    private void Die()
    {
        //Death Stuff here
        Destroy(gameObject);
    }
}
