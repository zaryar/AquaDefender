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
            Debug.Log(audioSource, audioSource.clip);
            audioSource.Play();

            animator.SetBool("isOpen", true);
            PlayerMovementController playerMovementController = player.GetComponent<PlayerMovementController>();
            playerMovementController.gotInvisibility = true;

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
        // Wait for 5 seconds
        chestText.text = "invisibility unlocked";
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        chestText.text = "";
    
    }
}

