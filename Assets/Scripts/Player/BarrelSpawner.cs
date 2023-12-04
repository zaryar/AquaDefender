using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    Transform barrelSpawnPoint;
    [SerializeField] GameObject barrel;
    [SerializeField] float reloadBarrel = 40f;

    bool reloading = false;


    private void Awake()
    {
        barrelSpawnPoint = GameObject.Find("BarrelSpawn").transform;

    }

    public void SpawnBarrel()
    {
        if (!reloading)
        {
            Instantiate(barrel, transform.position, barrelSpawnPoint.rotation);
            reloading = true;
            StartCoroutine(Cooldown(reloadBarrel));
        }
    }



    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
    }
}
