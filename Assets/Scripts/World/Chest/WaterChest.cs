using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterChest : Chest
{
    [HideInInspector] public Vector3 geyserSpawnPoint; // Der Spawnpoint des Geysirs
    [HideInInspector] public GameObject WaterGeyser; // Prefab des Wasser-Geysirs

    private float waittime;
    public float timeBeweenWater = 0.006f;
    public float restTime = 1.0f;
    private float eruptTime;
    private bool geyserOn = false;
     public float activeTime = 10.0f;
    public bool canUpdate = false;
    private AudioSource audioSource;
    private void Awake()
    {
        // Setze den Spawnpoint des Geysirs auf die Position dieses GameObjects
        geyserSpawnPoint = transform.position;
        audioSource = GetComponent<AudioSource>();

    }

    
    public void Update()
    {
        if(canUpdate)
        {
            waittime += Time.deltaTime;
            eruptTime += Time.deltaTime;

            if (eruptTime >= restTime)
            {
                geyserOn = true;
            }
            if (eruptTime >= (restTime + activeTime))
            {
                geyserOn = false;
                eruptTime = 0;
            }

            if (geyserOn)//waittime >= timeBeweenWater && geyserOn)
            {
                waittime = 0;
                InstantiateWaterGeyser();
            }
        }
    }

    void InstantiateWaterGeyser()
    {
        var waterOn = Instantiate(WaterGeyser, geyserSpawnPoint, Quaternion.identity);
        ChestWater water = waterOn.GetComponent<ChestWater>();
        water.InitializeWater(geyserSpawnPoint + new Vector3(0,1,0));
    }

     public void unlockFeature()
    {
        
            audioSource.Play();
        
    }



}
