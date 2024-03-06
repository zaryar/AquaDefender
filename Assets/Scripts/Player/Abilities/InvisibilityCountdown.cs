using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InvisibilityCountdown : MonoBehaviour
{
    //public Text invisibilityText;
    public Slider InvisibilitySlider;
    float countdownTime;
    int invisibleTime = 5; // same as invisibleTime in PlayerMovementController
    bool countdownActive = false;
    bool reloading = false;
    private float reloadingtime;
    //string state = "Invisibility: ";

    // Start is called before the first frame update
    void Start() 
    {
        countdownTime = invisibleTime;
        reloadingtime = invisibleTime * 6;
        InvisibilitySlider.value = InvisibilitySlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownActive)
        {
            countdownTime = countdownTime - Time.deltaTime;
            InvisibilitySlider.value = countdownTime * 6;
        }
        if (reloading)
        {
            countdownTime = countdownTime - Time.deltaTime;
            InvisibilitySlider.value = reloadingtime - countdownTime;   
        }
    }

    public void StartCountdown()
    {
        countdownActive = true;
    }

    public void StopCountdown()
    {
        countdownActive = false;
        reloading = true;             //starts reload
        countdownTime = reloadingtime;
        InvisibilitySlider.value = countdownTime;
    }


    public void StopReload()
    {
        StartCoroutine(Reload(countdownTime));
        InvisibilitySlider.value = reloadingtime;
    }

    protected IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
        countdownTime = invisibleTime;
    }
}

