using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chest : MonoBehaviour
{   
    [SerializeField]bool isOpen = false;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void openChest() {
        animator.SetBool("isOpen", true);
    }

    public void closeChest() {
        animator.SetBool("isOpen", false);
    }
}
