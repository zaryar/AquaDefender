using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public DragonAI boss;
    public Image healthBar;
    public Image healthBarBackground;
    public Text bossNameText;

    private void Start()
    {
        // Setzt den Namen des Bosses im Text-UI
        bossNameText.text = "Water Dragon";
        //HideHealthBar();
    }

    private void Update()
    {
        if (DragonAI.currentHealth > 0)
        {
            UpdateHealthBar();
            //ShowHealthBarUI();
        }
        else
        {
            HideHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        Debug.Log(DragonAI.currentHealth / boss.maxHealth);
        healthBar.fillAmount = DragonAI.currentHealth / boss.maxHealth;
    }
    public void ShowHealthBarUI()
    {
        healthBar.gameObject.SetActive(true);
        healthBarBackground.gameObject.SetActive(true);
        bossNameText.gameObject.SetActive(true);
    }

    void HideHealthBar()
    {
        healthBar.gameObject.SetActive(false);
        healthBarBackground.gameObject.SetActive(false);
        bossNameText.gameObject.SetActive(false); // Versteckt den Namen, wenn der Boss keine HP mehr hat
    }
    private void OnEnable()
    {
        DragonAI.BossSpawned += ShowHealthBarUI; // Auf das Event abonnieren
        
    }

    private void OnDisable()
    {
        DragonAI.BossSpawned -= ShowHealthBarUI; // Vom Event abmelden
        
    }
}
