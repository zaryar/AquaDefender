using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundController : MonoBehaviour
{
    public GameObject imageObject;
    public TextMeshProUGUI volumeText;

    void Start()
    {
        imageObject.SetActive(false);
        if (volumeText != null)
        {
            volumeText.text = "" + Mathf.RoundToInt(AudioListener.volume * 100) + "%";
        }
    }

    public void TurnSoundOn()
    {
        AudioListener.pause = false; // Sound einschalten
        imageObject.SetActive(false);
        if (Mathf.RoundToInt(AudioListener.volume * 100) == 0)
        {
            IncreaseVolume();
        }
    }

    public void TurnSoundOff()
    {
        AudioListener.pause = true; // Sound ausschalten
        imageObject.SetActive(true);
    }

    // Funktion zum Anpassen der Lautst�rke von 0% bis 100%
    public void AdjustVolume(float volumePercent)
    {
        float volume = Mathf.Clamp01(volumePercent / 100f);

        AudioListener.volume = volume;

        UpdateVolumeText();
    }

    // Erh�hen der Lautst�rke um 10%
    public void IncreaseVolume()
    {
        AdjustVolume((AudioListener.volume * 100) + 10);
        TurnSoundOn();
    }

    // Verringern der Lautst�rke um 10%
    public void DecreaseVolume()
    {
        AdjustVolume((AudioListener.volume * 100) - 10);
        if (Mathf.RoundToInt(AudioListener.volume * 100) > 0)
        {
            TurnSoundOn();
        }
        else
        {
            TurnSoundOff();
        }
    }

    private void UpdateVolumeText()
    {
        if (volumeText != null)
        {
            volumeText.text = "" + Mathf.RoundToInt(AudioListener.volume * 100) + "%";
        }
    }
}

