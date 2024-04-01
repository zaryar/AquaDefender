using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class Krakenhealthbar : MonoBehaviour
{
    // Start is called before the first frame update

    //public tentacles boss;
    public Image healthBar;
    public Image healthBarBackground;
    public Text bossNameText;
    GameObject boss;


    void Start()
    {
        bossNameText.text = "Kraken";
        boss = GameObject.Find("KrakenEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        boss = GameObject.Find("KrakenEnemy(Clone)");
        if (boss != null)
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
        float current_health = (float)boss.GetComponent<tentacles>().getHealth(); 
        float max_health = (float)boss.GetComponent<tentacles>().maximumHealth;
        healthBar.fillAmount = current_health / max_health;
        
        //Debug.Log(current_health / max_health); 
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
    /*private void OnEnable()
    {
        DragonAI.BossSpawned += ShowHealthBarUI; // Auf das Event abonnieren

    }

    private void OnDisable()
    {
        DragonAI.BossSpawned -= ShowHealthBarUI; // Vom Event abmelden

    }*/
}
