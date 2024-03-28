using UnityEngine;
using UnityEngine.UI;

public class LoadingIce : MonoBehaviour
{
    [HideInInspector] public Slider IceSlider; 
    [HideInInspector] public Graphic Volume;
    [HideInInspector] public Color Mint;
    [HideInInspector] public Graphic snow;

    public void Start(){

        IceSlider = GetComponentInChildren<Slider>();
        Volume = transform.Find("Volume").GetComponent<Graphic>();
        snow = transform.Find("snow").GetComponent<Graphic>();
        
        Mint = Volume.color;
        Volume.color = Color.grey;
        snow.color = Color.grey;
    }


    public void SetMaxEnergy(float energy){

        IceSlider.maxValue = energy;
        IceSlider.value = energy;
    }

    public void SetEnergy(float currentEnergy){

        IceSlider.value = currentEnergy;
    }
    
}