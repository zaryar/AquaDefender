using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IceChest : Chest
{
    public bool swordUnlocked = false;
    private AudioSource audioSource;
    public Text swordText;
    public float playerDetectionRadius = 5f;
    public float energyAmountToCharge = 5f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }


    public void unlockFeature()
    {
        
            //Debug.Log(audioSource, audioSource.clip);
            audioSource.Play();
            

            StartCoroutine(displayText());
        
        
    }

    IEnumerator displayText()
    {
        swordText.gameObject.SetActive(true);
        swordText.text = "New sword unlocked!";

        yield return new WaitForSeconds(3f); 

        swordText.text = "";
        swordText.gameObject.SetActive(false);
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Holen Sie sich den SwordTemplate des Spielers
            SwordTemplate swordTemplate = other.GetComponentInChildren<SwordTemplate>();
            if (swordTemplate != null)
            {
                // Lade die Energie des Ice Swords auf
                swordTemplate.ChargeEnergy(energyAmountToCharge);
            }
        }
    }

 }