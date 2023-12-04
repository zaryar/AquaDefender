using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CoinCounter : MonoBehaviour
{
    public int coinCount = 0;
    public AudioClip coinSound;
    public Text coinText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {

            CollectableItem collectableItem = other.GetComponent<CollectableItem>();
            if (collectableItem != null)
            {
                collectableItem.Collect();
                coinCount++;
                coinText.text = "Coins: " + coinCount.ToString();
            }
           
        }
    }
}