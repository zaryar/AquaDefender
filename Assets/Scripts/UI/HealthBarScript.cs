using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    public Health playerHealth;
    //private Image healthBar;
    public Slider HealthBarSlider;
    public float maxValue;


    void Start()
    {
        HealthBarSlider.maxValue = playerHealth.maxHealth;

    }

    void Update()
    {
        HealthBarSlider.maxValue = playerHealth.maxHealth;
        HealthBarSlider.value = playerHealth.currentHealth;
    }
}