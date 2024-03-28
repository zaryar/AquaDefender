using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IceBox : Chest
{
    [HideInInspector] public bool swordUnlocked = false;
    private AudioSource audioSource;
    [HideInInspector] public Text unlockText;
    [HideInInspector] public float energyAmountToCharge = 5f;
    [HideInInspector] public bool isPlayerInRange = false;
    [HideInInspector] public LoadingIce Bar;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        unlockText =  GameObject.Find("unlockText").GetComponent<Text>();
        Bar = GameObject.Find("LoadingIce").GetComponent<LoadingIce>();
    }


    public void unlockFeature()
    {
            //Debug.Log(audioSource, audioSource.clip);
            audioSource.Play();
            
            StartCoroutine(displayText());

            Bar.Volume.color = Bar.Mint;
            Bar.snow.color = Bar.Mint;
    }

    IEnumerator displayText()
    {
        unlockText.gameObject.SetActive(true);
        unlockText.text = "New sword unlocked! [click right]";

        yield return new WaitForSeconds(3f); 

        unlockText.text = "";
        unlockText.gameObject.SetActive(false);
    
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