using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject shopMenu;
    public GameObject AbilityUI;

    //Referenz
    public GameObject shopKeeperGO;


    // Reference for script
    private ShopKeeper shopKeeperScript;

    void Start()
    {
        // deactivate Pause-Menu by default
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (shopKeeperGO != null)
        {
            shopKeeperScript = shopKeeperGO.GetComponent<ShopKeeper>();
        }
    }

    void Update()
    {
        // Press ESC to pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(shopKeeperScript != null && shopKeeperScript.shopMenu.activeSelf)
            {
                // Rufe die Methode im ShopKeeper-Skript auf, um das Shop-Menü zu schließen
                shopKeeperScript.CloseShopMenu();
            }
            else
            {
                optionsMenu.SetActive(false);
                TogglePauseMenu();
            }
            
        }

    }

    public void TogglePauseMenu ()
    {
        // toggle
        if (pauseMenu != null)
        {
            bool isMenuActive = pauseMenu.activeSelf;
            if (isMenuActive)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Pause game
        pauseMenu.SetActive(true);
        AbilityUI.SetActive(false);
        Time.timeScale = 0f;
    }

    // BUTTON ONCLICK EVENT FUNCTIONS:
    public void ResumeGame()
    {
        // Resume game
        this.pauseMenu.SetActive(false);
        AbilityUI.SetActive(true);
        if (!shopMenu.activeSelf)
        { 
            Time.timeScale = 1f;
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
