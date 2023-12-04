using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectableItem
{
    public AudioClip coinSound;
    public float rotationSpeed = 50f;

    protected override void PlayCollectSound()
    {
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Coin";

    }

    // Update is called once per frame
    void Update()
    {
        // rotation after coin is dropped:
        transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);
    }

    
}
