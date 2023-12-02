using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTemplate : WeaponTemplate
{
    
    Transform bulletSpawnPoint;
    Transform waterBulletSpawnPoint;
    [SerializeField] GameObject ammunition;
    [SerializeField] GameObject waterAmmunition;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float waterBulletSpeed = 10;
    [SerializeField] float reloadTime = .5f;
    [SerializeField] float waterCannonFireRate = 20f;

    WaitForSeconds waterCannonFireWait; 
    public AudioClip[] clips;


    protected override void Awake()
    {
        
        bulletSpawnPoint = transform.Find("Muzzle").transform;
        waterCannonFireWait = new WaitForSeconds(1 / waterCannonFireRate);
    }

    public void Shoot()
    {
        if (!onCooldown)
        {
            int randomIndex = Random.Range(0, clips.Length);
            AudioSource.PlayClipAtPoint(clips[randomIndex], transform.position);
            var bullet = Instantiate(ammunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation, transform);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            onCooldown = true;
            StartCoroutine(base.Cooldown(reloadTime));
        }
    }

    public void WaterCannonShoot()
    {
        var waterBullet = Instantiate(waterAmmunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation, transform);
        waterBullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * waterBulletSpeed;
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
