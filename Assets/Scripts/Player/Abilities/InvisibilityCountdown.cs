using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InvisibilityCountdown : MonoBehaviour
{
    public Text invisibilityText;
    float countdownTime;
    int invisibleTime = 5; // same as invisibleTime in PlayerMovementController
    bool countdownActive = false;
    bool reloading = false;
    string state = "Invisibility: ";

    // Start is called before the first frame update
    void Start() 
    {
        countdownTime = invisibleTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownActive)
        {
            countdownTime = countdownTime - Time.deltaTime;
            state = "Invisible: ";
        }
        else if (reloading)
        {
            countdownTime = countdownTime - Time.deltaTime;
            invisibilityText.color = Color.red;
            state = "Reload: ";
            
            
        }
        TimeSpan time = TimeSpan.FromSeconds(countdownTime);
        invisibilityText.text = state + time.Seconds.ToString() + "s";
    }

    public void StartCountdown()
    {
        countdownActive = true;
    }

    public void StopCountdown()
    {
        countdownActive = false;
        reloading = true;             //starts reload
        countdownTime = invisibleTime * 6;
    }


    public void StopReload()
    {
        StartCoroutine(Reload(countdownTime));
        invisibilityText.text = "Invisibility" + countdownTime.ToString() + "s";
    }

    protected IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
        countdownTime = invisibleTime;
        invisibilityText.color = Color.white;
        state = "Invisibility: ";
    }
}

