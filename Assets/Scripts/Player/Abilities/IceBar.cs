using UnityEngine;
using UnityEngine.UI;

public class IceBar : MonoBehaviour
{
    public Slider energySlider; 
    public Graphic Ice;
    public Color lightBlue;
    public Graphic Snowflake;

    public void Start(){

        lightBlue = Ice.color;
        Ice.color = Color.grey;
        Snowflake.color = Color.grey;
    }

    public void SetMaxEnergy(float energy){

        energySlider.maxValue = energy;
        energySlider.value = energy;
    }

    public void SetEnergy(float currentEnergy){

        energySlider.value = currentEnergy;
    }
    
}