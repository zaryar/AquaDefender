using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private bool isSoundOn = true;
    public GameObject imageObject;

    void Start()
    {
        imageObject.SetActive(false);
    }

        public void TurnSoundOn()
    {
        isSoundOn = true;
        AudioListener.pause = false; // Sound einschalten
        imageObject.SetActive(false);
    }

    public void TurnSoundOff()
    {
        isSoundOn = false;
        AudioListener.pause = true; // Sound ausschalten
        imageObject.SetActive(true);
    }
}

