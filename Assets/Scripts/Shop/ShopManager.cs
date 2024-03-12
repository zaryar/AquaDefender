using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{

    
    public Text coinText;
    // using static variable CoinCounter.coins;

    public ShopItem[] shopItem; // all shop items in a list
    public GameObject[] shopPanelsGO; // GO reference
    public ShopTemplate[] shopPanels; // all items information // script reference
    public Button[] PurchaseButtons; // button list

    public GameObject shopMenu; // Shop Menu to hide in the beginning

    public AudioClip buySoundEffect;
    private AudioSource audioSource;

    public Health healthScript; // Referenz to Health.cs
    public PlayerMovementController playerMovementControllerScript; // Referenz to playerMovementControllerScript.cs
    public BarrelCounter barrelCounterScript; //yea still the same
    public BuildEscapeShip buildEscapeShipScript; //idk what to write at this point


    void Start()
    {
        CloseShopMenu(); // initially invisible shop-menu!
        initializeShopItemVisibility();
        updateCoinCounterTxt();
        LoadPanels();
        CheckPurchasable();
    }

    void Update()
    {
        CheckPurchasable();
        DeactivateBuildingShip();
        DeactivateBarrelUp();
    }

    public void initializeShopItemVisibility()
    {
        for (int i = 0; i < shopPanelsGO.Length; i++)
        {
            shopPanelsGO[i].SetActive(false); // initially all invisible
        }
        for (int i = 0; i < shopItem.Length; i++)
        {
            shopPanelsGO[i].SetActive(true); // visibility for the shop items
        }
    }

    public void updateCoinCounterTxt()
    {
        coinText.text = CoinCounter.coins.ToString();
    }

    public void CheckPurchasable()
    {
        // activate/deactivate buy-buttons for the items
        for (int i = 0; i < shopItem.Length; i++)
        {
            if(CoinCounter.coins >= shopItem[i].itemCost)
            {
                PurchaseButtons[i].interactable = true;
            } else
            {
                PurchaseButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int buttonId)
    {
        PlayBuyAudio();
        CoinCounter.coins = CoinCounter.coins - shopItem[buttonId].itemCost;
        updateCoinCounterTxt();
        CheckPurchasable();
        GivePurchasedItem(buttonId);
    }

    // Here the functionality of the purchasable Items is implemented
    public void GivePurchasedItem(int buttonId)
    {
        Debug.Log(shopItem[buttonId].ToString());
        switch (shopItem[buttonId].ToString())
        {
            case "Health (ShopItem)":
                Debug.Log("healing10");
                healthScript.Heal(10);
                break;
            case "PermHealth (ShopItem)":
                healthScript.maxHealth += 5;
                break;
            case "Armor (ShopItem)":
                healthScript.armor += 10;
                break;
            case "Boots (ShopItem)":
                playerMovementControllerScript.movementSpeed += 0.3f;
                break;
            case "BarelUp (ShopItem)":
                barrelCounterScript.plus1Barrel();
                break;
            case "shipinc (ShopItem)":
                    buildEscapeShipScript.SpawnShipPart();
                break;
            case "4 (ShopItem)":
                //
                break;
            case "5 (ShopItem)":
                //
                break;
            case "Boots":
                //
                break;
        }
    }

    public void CloseShopMenu()
    {
        if (this.shopMenu != null)
        {
            this.shopMenu.SetActive(false);
        }
        ResumeGame();
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItem.Length; i++)
        {
            shopPanels[i].itemNameTxt.text = shopItem[i].itemName;
            shopPanels[i].itemAmountTxt.text = shopItem[i].itemAmount;
            shopPanels[i].itemCostTxt.text = "" + shopItem[i].itemCost;
            shopPanels[i].itemIcon.sprite = shopItem[i].itemIcon;
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; 
    }

    // so complicated, because Game is paused at the moment
    public void PlayBuyAudio()
    {
        // initialize audioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buySoundEffect;

        audioSource.Stop();
        audioSource.time = 0;
        audioSource.Play();
    }

    void DeactivateBuildingShip()
    {
        if (buildEscapeShipScript.currentShipLvl > 6)
        {
            PurchaseButtons[7].interactable = false;
        }
    }

    void DeactivateBarrelUp()
    {
        if (barrelCounterScript.barrelCount == 5)
        {
            PurchaseButtons[1].interactable = false;
        }
    }



}
