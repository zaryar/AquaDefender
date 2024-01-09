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
                collectableItem.Collect();
                if (barrelCount <= 2)
                {
                    barrelCount++;
                    barrelText.text = "Barrels: " + barrelCount.ToString();
                }
                else
                {
                    if (barrelCount >= 3)
                    {
                        barrelCount = 3;
                    }
                }
            }

        }
    }

    public void DecreaseBarrelCount()
    {
        barrelCount--;
        barrelText.text = "Barrels: " + barrelCount.ToString();
    }

}
