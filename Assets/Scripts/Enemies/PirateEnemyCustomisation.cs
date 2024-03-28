using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateEnemyCustomisation : MonoBehaviour
{
    //Random Customisation for spawned Enemy Pirates
    [SerializeField] GameObject Hat1;
    [SerializeField] GameObject Hat2;
    [SerializeField] GameObject Hat3;
    [SerializeField] GameObject Chest1;
    [SerializeField] GameObject Chest2;

    void Start()
    {
        int chestIndex = Random.Range(0, 4);
        int hatIndex = Random.Range(0, 4);
        if (chestIndex != 1) { Chest1.SetActive(false); }
        if (chestIndex != 2 && chestIndex != 3) { Chest2.SetActive(false); }
        if (hatIndex != 1) { Hat1.SetActive(false); }
        if (hatIndex != 2) { Hat2.SetActive(false); }
        if (hatIndex != 3) { Hat3.SetActive(false); }
    }
}
