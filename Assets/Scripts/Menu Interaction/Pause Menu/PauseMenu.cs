using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    void Start()
    {
        // deactivate Pause-Menu by default
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        // Press P to pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenu.SetActive(false);
            TogglePauseMenu();
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
        Time.timeScale = 0f;
    }

    // BUTTON ONCLICK EVENT FUNCTIONS:
    public void ResumeGame()
    {
        // Resume game
        this.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
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
