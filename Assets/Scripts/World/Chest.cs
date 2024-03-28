using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    [SerializeField] bool isOpen = false;
    GameObject player;
    public static event Action OnChestOpened;
    private Animator animator;
    private AudioSource audioSource;
    public Text chestText;
    public InvisibilityCountdown InvisibilityScript;
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
            InvisibilityScript.Invisibility.color = Color.white;
            InvisibilityScript.Ghost.color = Color.white;
            // L�se das Event aus, wenn die Kiste ge�ffnet wird
            OnChestOpened?.Invoke();

            StartCoroutine(displayText());
        }
        isOpen = true;
    }

    public void closeChest()
    {
        animator.SetBool("isOpen", false);
    }

    

    IEnumerator displayText()
    {
        chestText.gameObject.SetActive(true);
        chestText.text = "Invisibility unlocked!";

        yield return new WaitForSeconds(3f); 

        chestText.text = "";
        chestText.gameObject.SetActive(false);
    
    }
}
