using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterChest : Chest
{
    public Vector3 geyserSpawnPoint; // Der Spawnpoint des Geysirs
    public GameObject WaterGeyser; // Prefab des Wasser-Geysirs

    private float waittime;
    public float timeBeweenWater = 0.006f;
    public float restTime = 3.5f;
    private float eruptTime;
    private bool geyserOn = false;
    public float activeTime = 3.5f;
    public bool canUpdate = false;
    
    private void Awake()
    {
        // Setze den Spawnpoint des Geysirs auf die Position dieses GameObjects
        geyserSpawnPoint = transform.position;
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
                Debug.Log("Wasser");
                waittime = 0;
                InstantiateWaterGeyser();
            }
        }
    }

    void InstantiateWaterGeyser()
    {
        var waterOn = Instantiate(WaterGeyser, geyserSpawnPoint, Quaternion.identity);
        WaterGeyser water = waterOn.GetComponent<WaterGeyser>();
        water.InitializeWater(geyserSpawnPoint + new Vector3(0,1,0));
    }
}
