using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    [SerializeField] protected bool isOpen = false;

    public static event Action OnChestOpened;
    private Animator animator;
    
   
    // public AudioClip chestOpen; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    
    }

    public void openChest()
    {

        if (!isOpen)
        {

            animator.SetBool("isOpen", true);
            OnChestOpened?.Invoke();

        }
        isOpen = true;

        if (this is InvisibilityChest)
        {
            Debug.Log("Hi");
            InvisibilityChest invisibilityChestInstance = this as InvisibilityChest;
            invisibilityChestInstance.unlockFeature();
        }
        Debug.Log(this is InvisibilityChest);
        
    }

    

    public void closeChest()
    {
        animator.SetBool("isOpen", false);
    }

    

}

