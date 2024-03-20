using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadSceneAsync("MainMenu");
    }
}