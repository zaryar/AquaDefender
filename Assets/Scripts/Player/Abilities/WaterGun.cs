using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using System.Collections;

public class WaterGun : MonoBehaviour
{
    public int water = 100;
    public AudioClip coinSound;
    //public Text waterText;
    public Slider slider;
    
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
                    slider.value = water;
            }
        }
    }

    public void removeWater()
    {
        water--;
        slider.value = water;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");
        waittime += Time.deltaTime;
        if (player.transform.position.y < 1 && waittime >= reloadTime)
        {
            waittime = 0;
            water++;
            if (water < 100)
                slider.value = water;
        }
    }
}