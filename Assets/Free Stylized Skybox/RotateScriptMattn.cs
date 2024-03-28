using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateSkybox : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Aktiviert die Skybox-Drehung, wenn die Szene geladen wird
        enabled = true;
    }

    void OnSceneUnloaded(Scene scene)
    {
        // Deaktiviert die Skybox-Drehung, wenn die Szene entladen wird
        enabled = false;
    }

    void Update()
    {
        // Rotiert die Skybox um die Y-Achse basierend auf der Zeit und der Rotationsgeschwindigkeit
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
