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
        UnityEngine.Debug.Log(damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        UnityEngine.Debug.Log("Dead");
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
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //Barrel ist auch als Enemy deklariert
        {
            TakeDamage(10);
        }
    }


}