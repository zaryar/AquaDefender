using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chest : MonoBehaviour
{   
    [SerializeField]bool isOpen = false;
    GameObject player;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void openChest() {
        animator.SetBool("isOpen", true);
        PlayerMovementController playerMovementController = player.GetComponent<PlayerMovementController>();
        playerMovementController.gotInvisibility = true;
    }

    public void closeChest() {
        animator.SetBool("isOpen", false);
    }
}
