using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class WaterGun : MonoBehaviour
{
    public int water = 100;
    public AudioClip coinSound;
    public Text waterText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaterDrop"))
        {

            CollectableItem collectableItem = other.GetComponent<CollectableItem>();
            if (collectableItem != null)
            {
                collectableItem.Collect();
                if (water > 80)
                    water = 100;
                else
                    water += 20;
                if(waterText != null)
                    waterText.text = "Water: " + water.ToString() + "%";
            }

        }
    }

    public void removeWater()
    {
        water--;
        if (waterText != null)
            waterText.text = "Water: " + water.ToString() + "%";
    }
}