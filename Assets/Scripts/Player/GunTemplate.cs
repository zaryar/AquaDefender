using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTemplate : MonoBehaviour
{
    Transform bulletSpawnPoint;
    Transform waterBulletSpawnPoint;
    [SerializeField] GameObject ammunition;
    [SerializeField] GameObject waterAmmunition;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float waterBulletSpeed = 10;
    [SerializeField] float reloadTime = .5f;
    [SerializeField] float waterReloadTime = .1f;

    bool _reloading = false;
    bool _waterReloading = false;

    private void Awake()
    {
        bulletSpawnPoint = transform.Find("Muzzle").transform;
        waterBulletSpawnPoint = transform.Find("WaterProjectileSpawn").transform;
    }

    public void Shoot()
    {
        if (!_reloading)
        {
            var bullet = Instantiate(ammunition, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            _reloading = true;
            StartCoroutine(Cooldown(reloadTime));
        }
    }

    public void waterCannonShoot()
    {
        if (!_waterReloading)
        {
            for (int i = 0; i < 10; i++)
            {
                var waterBullet = Instantiate(waterAmmunition, waterBulletSpawnPoint.position, waterBulletSpawnPoint.rotation);
                waterBullet.GetComponent<Rigidbody>().velocity = waterBulletSpawnPoint.forward * waterBulletSpeed;
                _waterReloading = true;
                StartCoroutine(CooldownW(waterReloadTime));
            }
        }
    }

    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
       _reloading = false;
    }

    private IEnumerator CooldownW(float time)
    {
        yield return new WaitForSeconds(time);
        _waterReloading = false;
    }
}
