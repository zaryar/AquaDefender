using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chest : MonoBehaviour
{   
    [SerializeField]bool isOpen = false;
    GameObject player;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip chestOpen; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void openChest() {
        animator.SetBool("isOpen", true);
        PlayerMovementController playerMovementController = player.GetComponent<PlayerMovementController>();
        playerMovementController.gotInvisibility = true;

        if (chestOpen != null && audioSource != null)
                {
                    audioSource.PlayOneShot(chestOpen);
                }

    }

    public void closeChest() {
        animator.SetBool("isOpen", false);
    }

    
}
