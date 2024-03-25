using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using System.Collections;

public class WaterGun : MonoBehaviour
{
    public int water = 100;
    public AudioClip coinSound;
    public Slider WaterSlider;

    public GameObject player;
    private float waittime;
    public float reloadTime = 2;

    public Graphic Water;
    public Color Blue;

    public void Start(){

        Blue = Water.color;

        Water.color = Color.grey;
    }


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
                    WaterSlider.value = water;
            }
        }
    }

    public void removeWater()
    {
        water--;
        WaterSlider.value = water;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");
        waittime += Time.deltaTime;
        if (player.transform.position.y < 1 && waittime >= reloadTime)
        {
            waittime = 0;
            water  += 5;
            if (water < 100)
            {
                WaterSlider.value = water;
            }
        }
    }

}