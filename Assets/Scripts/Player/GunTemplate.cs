using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTemplate : MonoBehaviour
{
    Transform bulletSpawnPoint;
    [SerializeField] GameObject ammunition;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float reloadTime = .5f;

    bool _reloading = false;

    private void Awake()
    {
        bulletSpawnPoint = transform.Find("Muzzle").transform;
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

    private IEnumerator Cooldown(float time)
    {
        _reloading = false;
        yield return new WaitForSeconds(time);
       
    }
}
