using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public bool lvl2unlocked = false;
    public Button lvl2Button;

    void Update()
    {
        if (lvl2Button != null)
            lvl2Button.interactable = lvl2unlocked;

        lvl2unlocked = PlayerPrefs.GetInt("Lvl2", 0) == 1;
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

    public void LoadCustom()
    {
        SceneManager.LoadSceneAsync("Customisation");
    }

    public void LoadLVL2()
    {
        if (lvl2unlocked)
        {
            SceneManager.LoadSceneAsync("Level2");
        }
    }
}