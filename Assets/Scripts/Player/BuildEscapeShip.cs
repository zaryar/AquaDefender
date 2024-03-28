using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class BuildEscapeShip : MonoBehaviour
{
    Transform shipSpawner;
    [SerializeField] public GameObject Gerippe;
    [SerializeField] public GameObject Planken;
    [SerializeField] public GameObject Deck;
    [SerializeField] public GameObject MastVorne;
    [SerializeField] public GameObject MastHinten;
    [SerializeField] public GameObject SegelVorne;
    [SerializeField] public GameObject SegelHinten;
    [SerializeField] private Animator shipAnimator; // Reference to the Animator component on the ship

    private bool hasDrivenAway = false; // Flag to check if the ship has already driven away
    private bool isBossDead = false;     


    // Definiere ein Event f�r das Wegfahren des Schiffes mit dem Typ Action
    public static event Action OnShipHasDeparted;


    public int currentShipLvl = 0;



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentShipLvl == 7 && isBossDead)
        {
            if (Input.GetKeyDown(KeyCode.F) && !hasDrivenAway)
            {
                DriveAway();
            }
        }
    }


    void DriveAway()
    {
        // Get a reference to the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Make the player invisible by disabling all their Renderer components
        Renderer[] playerRenderers = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in playerRenderers)
        {
            renderer.enabled = false;
        }

        // Parent the player to the ship so the player moves with the ship
        player.transform.SetParent(transform);

        // Play the drive away animation
        shipAnimator.Play("DriveAway");
        Debug.Log("You won!");
        OnShipHasDeparted?.Invoke();

        // Set the flag to true to prevent the animation from playing again
        hasDrivenAway = true;
        PlayerPrefs.SetInt("Lvl2", 1);

    }



    IEnumerator LoadVictoryScene()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Load the victory scene
        SceneManager.LoadSceneAsync("victory");
    }



    private void Awake()
    {
        shipSpawner = GameObject.Find("BuildingShipSpawner").transform;

    }


    public void SpawnShipPart()
    {
        GameObject newPart;

        switch (currentShipLvl)
        {
            case 0:
                newPart = Instantiate(Gerippe, transform.position + (0.85f * new Vector3(0, 1, 0)), shipSpawner.rotation);
                newPart.transform.SetParent(transform);
                currentShipLvl++;
                break;

            case 1:
                newPart = Instantiate(Planken, transform.position, shipSpawner.rotation);
                newPart.transform.SetParent(transform);
                currentShipLvl++;
                break;

            case 2:
                newPart = Instantiate(Deck, transform.position + (0.85f * (new Vector3(4.191336f, 4.130383f, 0))), shipSpawner.rotation);
                newPart.transform.SetParent(transform);
                currentShipLvl++;
                break;

            case 3:
                newPart = Instantiate(MastVorne, transform.position + (0.85f * new Vector3(-2.654286f, 3.585259f, 0)), shipSpawner.rotation);
                newPart.transform.SetParent(transform);
                currentShipLvl++;
                break;

            case 4:
                newPart = Instantiate(MastHinten, transform.position + (0.85f * new Vector3(1.726996f, 3.188487f, 0)), shipSpawner.rotation);
                newPart.transform.SetParent(transform);
                currentShipLvl++;
                break;

            case 5:
                newPart = Instantiate(SegelVorne, transform.position + (0.85f * new Vector3(-2.310965f, -0.7652126f, 0)), shipSpawner.rotation);
                newPart.transform.SetParent(transform);
                currentShipLvl++;
                break;

            case 6:
                newPart = Instantiate(SegelHinten, transform.position + (0.85f * new Vector3(2.051623f, 0, 0)), shipSpawner.rotation);
                newPart.transform.SetParent(transform);
                currentShipLvl++;
                break;
        }
    }
    private void OnEnable()
    {
        DragonAI.OnBossDeath += HandleBossDeath;
    }

    private void OnDisable()
    {
        DragonAI.OnBossDeath -= HandleBossDeath;
    }

    private void HandleBossDeath()
    {
        isBossDead = true;
    }


}
