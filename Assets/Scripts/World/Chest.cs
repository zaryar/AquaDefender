using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    [SerializeField] bool isOpen = false;
    GameObject player;
    private Animator animator;
    private AudioSource audioSource;
    public Text chestText;
    // public AudioClip chestOpen; 
    // Start is called before the first frame update
    public InvisibilityCountdown InvisibilityScript;

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

