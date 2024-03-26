using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using System.Collections;

public class WaterGun : MonoBehaviour
{
    public int water = 100;
    public AudioClip coinSound;

    public GameObject player;
    private float waittime;
    public float reloadTime = 2;
    
    [HideInInspector] public Slider WaterSlider;
    [HideInInspector] public Graphic Water;
    [HideInInspector] public Color Blue;
    [HideInInspector] public Graphic DropImage;

    public void Start()
    {

        if (WaterSlider != null && Water != null)
        {
            WaterSlider.value = water;
            Water.color = Color.grey;
            DropImage.color = Color.grey;
        }

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
            water += 5;
            if (water < 100)
            {
                WaterSlider.value = water;
            }
        }
    }

}