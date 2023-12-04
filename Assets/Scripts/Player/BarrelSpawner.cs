using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    Transform barrelSpawnPoint;
    [SerializeField] GameObject barrel;
    [SerializeField] float reloadBarrel = 40f;
    [SerializeField] Vector3 spawnCheckSize = new Vector3(0.1f, 0.1f, 0.1f); // Größe der Überprüfungsbox

    bool reloading = false;

    private void Awake()
    {
        barrelSpawnPoint = GameObject.Find("BarrelSpawn").transform;
    }

    public void SpawnBarrel()
    {
        if (!reloading && CanSpawnBarrel())
        {
            Instantiate(barrel, transform.position, barrelSpawnPoint.rotation);
            reloading = true;
            StartCoroutine(Cooldown(reloadBarrel));
        }
    }


    bool CanSpawnBarrel()
    {
        Collider[] hitColliders = Physics.OverlapBox(barrelSpawnPoint.position, spawnCheckSize / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.gameObject != gameObject && hitCollider.tag != "Player")
            {

                return false;
            }
        }
        return true;
    }

    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
    }
}
