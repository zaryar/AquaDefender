using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponTemplate : MonoBehaviour {

    [SerializeField] protected Transform attackTransform;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    string[] playerFraction = {"Player"};
    string[] enemyFraction = {"Enemy"};
    protected string[] opposingFraction;

    protected bool onCooldown = false;

     protected virtual void Awake()
    {
        attackTransform = GameObject.Find("Muzzle").transform;
        if (gameObject.name.Contains("Gun"))
        {
            attackTransform = gameObject.transform.Find("Muzzle");

        }
        else if (gameObject.name.Contains("Sword"))
        {
            attackTransform = gameObject.transform.parent.transform;
        }
        
        checkOpposingFraction();
       
    }

    
    public virtual void Attack()
    {
        // Allgemeine Implementierung für alle Waffen, falls benötigt
    }

    protected void checkOpposingFraction() 
    {
        Transform parentTransform = transform.parent;

        while(parentTransform!= null){
            string parentTag = parentTransform.tag;
            if (playerFraction.Contains(parentTag)){
                opposingFraction = enemyFraction;
                break;        
            }
            else if (enemyFraction.Contains(parentTag)){
                opposingFraction = playerFraction;
                break;      
            }
            parentTransform = parentTransform.parent;
        }
        //Debug.Log(transform.name + opposingFraction);
    }

     public void setOpposingFraction(string[] fraction)
    {
        opposingFraction = fraction;

    }



    protected IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }
}