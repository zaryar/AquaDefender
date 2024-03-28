using UnityEngine;
using UnityEngine.UI;

public class LoadingIce : MonoBehaviour
{
    [HideInInspector] public Slider IceSlider; 
    [HideInInspector] public Graphic Volume;
    [HideInInspector] public Color Mint;
    [HideInInspector] public Graphic snow;
    [HideInInspector] public float iceEnergy;


    public void Start(){

        IceSlider = transform.GetComponent<Slider>();
        Volume = transform.Find("Volume").GetComponent<Graphic>();
        snow = transform.Find("snow").GetComponent<Graphic>();
        
        IceSlider.maxValue = iceEnergy;
        IceSlider.value = iceEnergy;
        
        Mint = Volume.color;
        Volume.color = Color.grey;
        snow.color = Color.grey;
    }


    public void SetMaxEnergy(float energy){
        iceEnergy = energy;
    }

    public void SetEnergy(float currentEnergy){

        IceSlider.value = currentEnergy;
    }
    
}