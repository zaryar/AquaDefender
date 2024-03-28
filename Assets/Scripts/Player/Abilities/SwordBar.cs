using UnityEngine;
using UnityEngine.UI;

public class SwordBar : MonoBehaviour
{
    [HideInInspector] public Slider energySlider; 
    [HideInInspector] public Graphic Ice;
    [HideInInspector] public Color lightBlue;
    //[HideInInspector] public Graphic Snowflake;

    public void Start(){

        energySlider = GetComponentInChildren<Slider>();
        Ice = transform.Find("Ice").GetComponent<Graphic>();
        //Snowflake = transform.Find("Snowflake").GetComponent<Graphic>();
        
        lightBlue = Ice.color;
        Ice.color = Color.grey;
        //Snowflake.color = Color.grey;
    }


    public void SetMaxEnergy(float energy){

        energySlider.maxValue = energy;
        energySlider.value = energy;
    }

    public void SetEnergy(float currentEnergy){

        energySlider.value = currentEnergy;
    }
    
}