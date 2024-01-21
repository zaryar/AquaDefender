using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if Player is pressing key "F" + is in Radius of Shopkeeper
        if (Input.GetKeyDown(KeyCode.F) && IsPlayerInRadius())
        {
            ToggleShopMenu();
        }
    }

    public float interactionRadius = 0.2f;
    public GameObject shopMenu; // GO which represents the Shop-Menu

    bool IsPlayerInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void ToggleShopMenu()
    {
        if (shopMenu.activeSelf)
        {
            CloseShopMenu();
        }
        else
        {
            OpenShopMenu();
        }
    }

    void OpenShopMenu()
    {
        // Activate Shop-Menu-GO
        shopMenu.SetActive(true);
        PauseGame();
    }

    void CloseShopMenu()
    {
        // Deactivate Shop-Menu-GO
        shopMenu.SetActive(false);
        ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }

}
