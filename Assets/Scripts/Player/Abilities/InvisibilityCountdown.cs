using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InvisibilityCountdown : MonoBehaviour
{
    public Graphic Ghost;
    public Graphic Invisibility;
    public Slider InvisibilitySlider;
    float countdownTime;
    int invisibleTime = 5; // same as invisibleTime in PlayerMovementController
    bool countdownActive = false;
    bool reloading = false;
    private float reloadingtime;

    // Start is called before the first frame update
    void Start() 
    {
        Ghost.color = Color.gray;
        Invisibility.color = Color.grey;
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
    }


    public void StopReload()
    {
        StartCoroutine(Reload(countdownTime));
    }

    protected IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
        countdownTime = invisibleTime;
    }
}

