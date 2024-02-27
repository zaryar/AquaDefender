using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    Transform barrelSpawnPoint;
    [SerializeField] public GameObject barrel;
    [SerializeField] Vector3 spawnCheckSize = new Vector3(1.0f, 0.1f, 1.0f); // Gr??e der ?berpr?fungsbox

    public GameObject player;

    private void Awake()
    {
        barrelSpawnPoint = GameObject.Find("BarrelSpawn").transform;
    }

    public void SpawnBarrel()
    {
        

        if (player.GetComponent<BarrelCounter>().barrelCount > 0 && CanSpawnBarrel())
        {
            Instantiate(barrel, transform.position, barrelSpawnPoint.rotation);
            player.GetComponent<BarrelCounter>().DecreaseBarrelCount();
        }
    }


    bool CanSpawnBarrel()
    {
        Collider[] hitColliders = Physics.OverlapBox(barrelSpawnPoint.position, spawnCheckSize / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.gameObject != gameObject && hitCollider.tag != "Player" && hitCollider.tag != "Ground" && hitCollider.tag != "Water" && hitCollider.tag != "Objekt")
            {
                UnityEngine.Debug.Log($"Spawn Point: {barrelSpawnPoint.position}");
                UnityEngine.Debug.Log($"Spawn Check Size: {spawnCheckSize}");
                UnityEngine.Debug.Log($"Spawn Check Size: {hitCollider.tag}");


                return false;
            }
        }
        return true;
    }

}
