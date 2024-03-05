using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chest : MonoBehaviour
{
    [SerializeField] bool isOpen = false;
    GameObject player;
    private Animator animator;
    private AudioSource audioSource;
    // public AudioClip chestOpen; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    public void openChest()
    {

        if (!isOpen)
        {
            //Debug.Log(audioSource, audioSource.clip);
            audioSource.Play();

            animator.SetBool("isOpen", true);
            PlayerMovementController playerMovementController = player.GetComponent<PlayerMovementController>();
            playerMovementController.gotInvisibility = true;
        }
        isOpen = true;
    }

    public void closeChest()
    {
        animator.SetBool("isOpen", false);
    }

    

}
