using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelCounter : MonoBehaviour
{
    public Graphic[] barrelImages = new Graphic[5];
    public int barrelCount = 0;


    private void Start()
    {
        for (int i = 1; i <= 5; i++)
        {
            barrelImages[i - 1].color = Color.black;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BarrelCoin"))
        {

            CollectableItem collectableItem = other.GetComponent<CollectableItem>();
            if (collectableItem != null)
            {
                collectableItem.Collect();
                if (barrelCount < 5)
                {
                    barrelCount++;
                    barrelImages[barrelCount - 1].color = Color.white;
                }
            }

        }
    }

    public void DecreaseBarrelCount()
    {
        barrelCount--;
        barrelImages[barrelCount].color = Color.black;

    }

    public void plus1Barrel()
    {
        if (barrelCount < 5)
        {
            barrelCount++;
            barrelImages[barrelCount - 1].color = Color.white;
        }

    }

}
