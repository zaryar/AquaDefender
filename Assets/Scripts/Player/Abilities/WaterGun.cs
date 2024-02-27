using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using System.Collections;

public class WaterGun : MonoBehaviour
{
    public int water = 100;
    public AudioClip coinSound;
    public Text waterText;
    
    public GameObject player;
    private float waittime;
    public float reloadTime = 2;


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

    void Update()
    {
        player = GameObject.FindWithTag("Player");
        waittime += Time.deltaTime;
        if (player.transform.position.y < 1 && waittime >= reloadTime)
        {
            waittime = 0;
            water++;
            if (waterText != null && water < 100)
                waterText.text = "Water: " + water.ToString() + "%";
        }
    }

}