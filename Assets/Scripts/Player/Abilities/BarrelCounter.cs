using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelCounter : MonoBehaviour
{
    public Text barrelText;
    public int barrelCount = 0;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BarrelCoin"))
        {

            CollectableItem collectableItem = other.GetComponent<CollectableItem>();
            if (collectableItem != null)
            {

                barrelCount++;
                barrelText.text = "Barrels: " + barrelCount.ToString();

            }

        }
    }

    public void DecreaseBarrelCount()
    {
        barrelCount--;
        barrelText.text = "Barrels: " + barrelCount.ToString();
    }

    public void plus1Barrel()
    {
        barrelCount++;
        barrelText.text = "Barrels: " + barrelCount.ToString();
    }

}
