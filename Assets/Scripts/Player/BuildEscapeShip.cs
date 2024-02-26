using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int currentShipLvl = 0;



    

    private void Awake()
    {
        shipSpawner = GameObject.Find("BuildingShipSpawner").transform;

    }


    public void SpawnShipPart()
    {
        switch (currentShipLvl)
        {
            case 0:
                Instantiate(Gerippe, transform.position + (0.85f * new Vector3(0, 1, 0)) , shipSpawner.rotation);
                currentShipLvl++;
                break;

            case 1:
                Instantiate(Planken, transform.position, shipSpawner.rotation);
                currentShipLvl++;
                break;

            case 2:
                Instantiate(Deck, transform.position + (0.85f * (new Vector3(4.191336f, 4.130383f, 0))) , shipSpawner.rotation);
                currentShipLvl++;
                break;

            case 3:
                Instantiate(MastVorne, transform.position + (0.85f * new Vector3(-2.654286f, 3.585259f, 0)) , shipSpawner.rotation);
                currentShipLvl++;
                break;

            case 4:
                Instantiate(MastHinten, transform.position + (0.85f * new Vector3(1.726996f, 3.188487f, 0)), shipSpawner.rotation);
                currentShipLvl++;
                break;

            case 5:
                Instantiate(SegelVorne, transform.position + (0.85f * new Vector3(-2.310965f, -0.7652126f, 0)), shipSpawner.rotation);
                currentShipLvl++;
                break;

            case 6:
                Instantiate(SegelHinten, transform.position + (0.85f * new Vector3(2.051623f, 0, 0)), shipSpawner.rotation);
                currentShipLvl++;
                break;

        }
    }


}
