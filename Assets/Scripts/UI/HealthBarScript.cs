using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    public Health playerHealth;
    private Image healthBar;
    public TextMeshProUGUI healthText;

    void Start()
    {
        healthBar = GetComponent<Image>();

    }

    void Update()
    {
        if (playerHealth != null)
        {
            float healthPercent = (float)playerHealth.currentHealth / playerHealth.maxHealth;
            healthBar.fillAmount = healthPercent;
            healthText.text = $"{playerHealth.currentHealth} / {playerHealth.maxHealth}";
        }
    }
}