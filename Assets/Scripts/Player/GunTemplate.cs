using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTemplate : MonoBehaviour
{
    
    Transform bulletSpawnPoint;
    [SerializeField] GameObject ammunition;
    [SerializeField] GameObject waterAmmunition;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float waterBulletSpeed = 10;
    [SerializeField] float reloadTime = .5f;
    [SerializeField] float waterCannonFireRate = 20f;

    WaitForSeconds waterCannonFireWait; 
    public AudioClip[] clips;

    bool _reloading = false;

    private void Awake()
    {
        bulletSpawnPoint = transform.Find("Muzzle").transform;
        waterCannonFireWait = new WaitForSeconds(1 / waterCannonFireRate);
    }

    public void Shoot()
    {
        if (!_reloading)
        {
            int randomIndex = Random.Range(0, clips.Length);
            AudioSource.PlayClipAtPoint(clips[randomIndex], transform.position);
            var bullet = Instantiate(ammunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            _reloading = true;
            StartCoroutine(Cooldown(reloadTime));
        }
    }

    public void WaterCannonShoot()
    {
        var waterBullet = Instantiate(waterAmmunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        waterBullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * waterBulletSpeed;
    }

    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
       _reloading = false;
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
