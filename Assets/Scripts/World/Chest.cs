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
            //OnChestOpened?.Invoke();

        
            isOpen = true;

            if (this is TransparentBox)
            {
                TransparentBox TransparentBoxInstance = this as TransparentBox;
                TransparentBoxInstance.unlockFeature();
            }

            if (this is IceBox){
                IceBox IceBox = this as IceBox;
                IceBox.swordUnlocked = true;
                IceBox.unlockFeature();
            }
        }
        
    }

    

    public void closeChest()
    {
        animator.SetBool("isOpen", false);
    }

    

}

