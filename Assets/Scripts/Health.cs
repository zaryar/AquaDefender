using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int armor = 0; // R?stungswert
    public float criticalHitChance = 0.1f; // 10% Chance f?r kritische Treffer
    public int criticalHitMultiplier = 2; // Kritische Treffer schaden M;ultiplikator
    public Text deathText;

    [SerializeField] Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private bool canTakeDamage = true;

    public IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.3f);
        canTakeDamage = true;
    }

    public void TakeDamage(int damage, bool isSpecialAttack = false)
    {
        if (canTakeDamage)
        {
            /*if (UnityEngine.Random.value < criticalHitChance) 
            {
                damage *= criticalHitMultiplier;
                UnityEngine.Debug.Log("Crit");
            }*/

            damage = (int)((float)damage * (100.0 / (100.0 + (float)armor)));
            if (damage <= 1)
            {
                damage = 1;
            }
            //UnityEngine.Debug.Log(damage);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            StartCoroutine(DamageCooldown());
        }
    }

    void Die()
    {
        StartCoroutine(DieCoroutine());


    }

    IEnumerator DieCoroutine()
    {
        // Wait for 5 seconds
        deathText.text = "you're dead";
        Time.timeScale = 0;
        GameController.instance.PlayerDeath.Invoke();
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        deathText.text = "";
        SceneManager.LoadSceneAsync("MainMenu");
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
    /*void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&
            collision.gameObject.name != "Barrel1(Clone)" &&
            collision.gameObject.name != "Barrel1" &&
            collision.gameObject.name != "Barrel1(1)") //Barrel ist auch als Enemy deklariert
        {
            TakeDamage(10);
        }
    }*/


}