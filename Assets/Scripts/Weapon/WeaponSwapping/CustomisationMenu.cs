using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomisationMenu : MonoBehaviour
{
    public SwordModelSwapper sms;
    private int _currentsword;
    public GameObject Hat1;
    public GameObject Hat2;
    public GameObject Hat3;
    private int _hat = 0;
    public GameObject Beard1;
    public GameObject Beard2;

    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Customisation") { sms.enableRenderer(); }
        _currentsword = PlayerPrefs.GetInt("Sword", 1);
        _currentsword = ((_currentsword + 1) % 3) + 1;
        swapSword();
        _hat = PlayerPrefs.GetInt("Hat", 1);
        _hat = (_hat + 3) % 4;
        swapHat();
        int b = PlayerPrefs.GetInt("Beard", 0);
        Beard1.SetActive(false);
        Beard2.SetActive(false);
        if(b == 1 || b == 3) { Beard1.SetActive(true); }
        if(b== 2 || b == 3) { Beard2.SetActive(true); }
    }

    public void BackToMainMenu()
    {
        PlayerPrefs.SetInt("Sword", _currentsword);
        PlayerPrefs.SetInt("Hat", _hat);
        PlayerPrefs.SetInt("Beard", (Beard1.activeInHierarchy ? 1 : 0) + (Beard2.activeInHierarchy ? 2 : 0));
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void swapSword()
    {
        switch (_currentsword)
        {
                case 1:
                sms.swapModel(Weapons.Swords.sword2);
                _currentsword= 2;
                break;
                case 2:
                sms.swapModel(Weapons.Swords.sword3);
                _currentsword = 3;
                break;
                case 3:
                sms.swapModel(Weapons.Swords.sword1);
                _currentsword = 1;
                break;
        }
    }
    public void toggleBeard1()
    {
        Beard1.SetActive(!Beard1.activeInHierarchy);
    }
    public void toggleBeard2()
    {
        Beard2.SetActive(!Beard2.activeInHierarchy);
    }

    public void swapHat()
    {
        switch (_hat)
        {
            case 0:
                Hat1.SetActive(true);
                Hat2.SetActive(false);
                Hat3.SetActive(false);
                break;
            case 1:
                Hat1.SetActive(false);
                Hat2.SetActive(true);
                Hat3.SetActive(false);
                break;
            case 2:
                Hat1.SetActive(false); 
                Hat2.SetActive(false);
                Hat3.SetActive(true);
                break;
            case 3:
                Hat1.SetActive(false);
                Hat2.SetActive(false);
                Hat3.SetActive(false);
                break;
        }
        _hat = (_hat+1) % 4;
    }
}
