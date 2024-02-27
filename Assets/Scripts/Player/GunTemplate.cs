using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTemplate : WeaponTemplate
{
    
    Transform bulletSpawnPoint;
    [SerializeField] GameObject ammunition;
    [SerializeField] GameObject waterAmmunition;
    [SerializeField] float bulletSpeed = 20;
    [SerializeField] float waterBulletSpeed = 20;
    [SerializeField] float reloadTime = .5f;
    [SerializeField] float waterCannonFireRate = 20f;
    public GameObject player; // Assign the player instance in the Unity Editor for the watergun

    WaitForSeconds waterCannonFireWait; 
    public AudioClip[] clips;


    protected override void Awake()
    {
        checkOpposingFraction();
        bulletSpawnPoint = transform.Find("Muzzle").transform;
        waterCannonFireWait = new WaitForSeconds(1 / waterCannonFireRate);
    }

    public void Shoot()
    {
        if (!onCooldown)
        {
            int randomIndex = Random.Range(0, clips.Length);
            AudioSource.PlayClipAtPoint(clips[randomIndex], transform.position);
            var bullet = Instantiate(ammunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<WeaponTemplate>().setOpposingFraction(opposingFraction);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            onCooldown = true;
            StartCoroutine(base.Cooldown(reloadTime));
        }
    }

    public void WaterCannonShoot()
    {
        if (player.GetComponent<WaterGun>().water > 0 && !onCooldown)
        {
            var waterBullet = Instantiate(waterAmmunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            waterBullet.GetComponent<WeaponTemplate>().setOpposingFraction(opposingFraction);
            waterBullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * waterBulletSpeed;
            player.GetComponent<WaterGun>().removeWater();
            onCooldown = true;
            StartCoroutine(base.Cooldown((reloadTime*2)/waterCannonFireRate));
        }
    }

    
    public IEnumerator FireWaterCannon()
    {
        while (true)
        {
            WaterCannonShoot();
            yield return waterCannonFireWait;
        }
    }
}
