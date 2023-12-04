using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int armor = 0; // Rüstungswert
    public float criticalHitChance = 0.1f; // 10% Chance für kritische Treffer
    public int criticalHitMultiplier = 2; // Kritische Treffer schaden M;ultiplikator
    [SerializeField] GameObject HitParticle;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, bool isSpecialAttack = false)
    {
        /*if (UnityEngine.Random.value < criticalHitChance) 
        {
            damage *= criticalHitMultiplier;
            UnityEngine.Debug.Log("Crit");
        }*/

        damage -= armor;
        if (damage < 0)
        {
            damage = 0;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //UnityEngine.Debug.Log("Dead");
        Destroy(gameObject);
    }


    public void IncreaseArmor(int amount)
    {
        armor += amount;
    }


    // Optional: Methode zur Heilung
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
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