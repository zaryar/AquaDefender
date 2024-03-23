using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IceChest : Chest
{
    public bool swordUnlocked = false;
    private AudioSource audioSource;
    public Text swordText;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }


    public void unlockFeature()
    {
        
            //Debug.Log(audioSource, audioSource.clip);
            audioSource.Play();
            

            //StartCoroutine(displayText());
        
        
    }

 }