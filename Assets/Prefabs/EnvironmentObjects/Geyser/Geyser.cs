using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{

    Transform geyserSpawnPoint;
    public Vector3[] SpawnPoints = new Vector3[16];
    public GameObject WaterGeyser;
    private float waittime;
    public float timeBeweenWater = 0.006f;
    public float restTime = 3.5f;
    private float eruptTime;
    private bool geyserOn = false;
    public float activeTime = 3.5f;
    private Vector3[] ActiveGeysers = new Vector3[3];
    private float setVector;
    private int index;
    


    private void Update()
    {
        ////Timer, spukt alle 0.01 sekunden für
        waittime += Time.deltaTime;
        //für restTime aus, für activeTime an
        eruptTime += Time.deltaTime;
        //Timer Spawner
        setVector += Time.deltaTime;

        if (setVector >= restTime)
        {
            index = Random.Range(1, SpawnPoints.Length - 1);

            ActiveGeysers[0] = SpawnPoints[index];
            ActiveGeysers[1] = SpawnPoints[index-1];
            ActiveGeysers[2] = SpawnPoints[index+1];
            setVector = 0;
        }

        if (eruptTime >= restTime)
        {
            geyserOn = true;
        }
        if (eruptTime >= (restTime + activeTime))
        {

            geyserOn = false;
            eruptTime = 0;
            setVector = 0;
        }


        if (waittime >= timeBeweenWater && geyserOn)
        {
            waittime = 0;
            for (int i = 0; i <= 2; i++) { 
            var waterOn = Instantiate(WaterGeyser, ActiveGeysers[i], Quaternion.identity); //
            WaterGeyser water = waterOn.GetComponent<WaterGeyser>();
            water.InitializeWater(ActiveGeysers[i] + new Vector3(0,1,0)); //Rigidbody, funktioniert nicht
            }
        }   
            
    }



}
