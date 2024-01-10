using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCoin : CollectableItem

{
    public AudioClip coinSound;
    public float rotationSpeed = 25f;

    protected override void PlayCollectSound()
    {
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "BarrelCoin";

    }

    // Update is called once per frame
    void Update()
    {
        // rotation after coin is dropped:
        transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);
    }


}
