using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public bool lvl2unlocked = false;
    public Button lvl2Button;

    void Update()
    {
        lvl2Button.interactable = lvl2unlocked;
    }

    public void LoadMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LoadLVL1()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void LoadLVLSelector()
    {
        SceneManager.LoadSceneAsync("lvlSelector");
    }

    public void LoadLVL2()
    {
        if (lvl2unlocked)
        {
            SceneManager.LoadSceneAsync("Level2");
        }
    }
}