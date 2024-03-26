using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    public int camper_player_id = 0;
    public int patrol_player_id = 0;
    public UnityEvent PlayerDeath;

    [HideInInspector] public float MovespeedFactor;
    [HideInInspector] public int BonusHealth;
    [HideInInspector] public float CritChance;
    [HideInInspector] public float CritDmg;
    [HideInInspector] public int Armor;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        instance.Save();
        PlayerPrefs.Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MovespeedFactor", MovespeedFactor);
        PlayerPrefs.SetInt("BonusHealth", BonusHealth);
        PlayerPrefs.SetFloat("CritChance", CritChance);
        PlayerPrefs.SetFloat("CritDmg", CritDmg);
        PlayerPrefs.SetInt("Armor", Armor);
        PlayerPrefs.SetInt("Coins", CoinCounter.coins);
    }
    public void Load()
    {

        //Load PLayerprefs
        MovespeedFactor = PlayerPrefs.GetFloat("MovespeedFactor", 1);
        BonusHealth = PlayerPrefs.GetInt("BonusHealth", 0);
        CritChance = PlayerPrefs.GetFloat("CritChance", 0);
        CritDmg = PlayerPrefs.GetFloat("CritDmg", 0);
        Armor = PlayerPrefs.GetInt("Armor", 0);
        //Get player health
        CoinCounter.coins = PlayerPrefs.GetInt("Coins", 0);
        GameObject player = GameObject.FindWithTag("Player");
        Health healthScript = player.GetComponent<Health>();
        if (healthScript != null)
        {
            healthScript.maxHealth += BonusHealth;
            healthScript.armor = Armor;
        }
    }
}
