using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IceChest : Chest
{
    public bool swordUnlocked = false;
    private AudioSource audioSource;
    public Text swordText;
    public float energyAmountToCharge = 5f;
    public bool isPlayerInRange = false;
    public IceBar Bar;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }


    public void unlockFeature()
    {
            //Debug.Log(audioSource, audioSource.clip);
            audioSource.Play();
            
            StartCoroutine(displayText());

            Bar.Ice.color = Bar.lightBlue;
    }

    IEnumerator displayText()
    {
        swordText.gameObject.SetActive(true);
        swordText.text = "New sword unlocked! [click right]";

        yield return new WaitForSeconds(3f); 

        swordText.text = "";
        swordText.gameObject.SetActive(false);
    
    }

    // Überprüfen Sie die Kollision mit dem Spieler
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& swordUnlocked)
        {
                isPlayerInRange = true;
                //Debug.Log("In Range"+isPlayerInRange);
                SwordTemplate swordTemplate = other.GetComponentInChildren<SwordTemplate>();
                if (swordTemplate != null)
                {
                    StartCoroutine(swordTemplate.ChargeEnergy(energyAmountToCharge));
                }
           
        }
    }

    private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        isPlayerInRange = false; 
    }
}


 }