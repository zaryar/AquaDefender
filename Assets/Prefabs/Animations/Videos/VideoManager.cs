using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Für den Zugriff auf die VideoPlayer-Komponente

public class VideoManager : MonoBehaviour
{
    [SerializeField] private GameObject videoGameObject; // Referenz zum Video-GameObject
    [SerializeField] private GameObject rawImage; // Referenz zum UI-GameObject, das das Video anzeigt

    void Start()
    {
        if (rawImage != null) // Sicherstellen, dass rawImage zugewiesen wurde
        {
            rawImage.SetActive(false);
        }
    }

    IEnumerator ShowCutsceneToLevel2()
    {
        Debug.Log(" Cutscene");
        // Warte auf das Ende der Animation
        yield return new WaitForSeconds(2.0f);

        // Stelle sicher, dass videoGameObject und rawImage zugewiesen wurden
        if (videoGameObject != null && rawImage != null)
        {
            // Aktiviere das Video und das Image
            videoGameObject.SetActive(true);
            rawImage.SetActive(true);

            VideoPlayer videoPlayer = videoGameObject.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }
            else
            {
                Debug.LogError("VideoPlayer-Komponente fehlt auf dem videoGameObject.");
            }

            
            while (videoPlayer != null && videoPlayer.isPlaying)
            {
                yield return null;
            }
            yield return new WaitForSeconds(6.0f);
            SceneManager.LoadScene("Level2");

            // Deaktiviere das Video und das Image
            rawImage.SetActive(false);
            videoGameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("videoGameObject oder rawImage wurde nicht zugewiesen.");
        }
    }

    private void OnEnable()
    {

        BuildEscapeShip.OnShipHasDeparted += ShipDepartureHandler;
    }

    private void OnDisable()
    {

        BuildEscapeShip.OnShipHasDeparted -= ShipDepartureHandler;
    }

    private void ShipDepartureHandler()
    {
        Debug.Log("Jetzt");
        StartCoroutine(ShowCutsceneToLevel2()); // Starte die Coroutine
    }
}
