using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CoinCounter : MonoBehaviour
{
    public static int coins = 1000;
    public Text coinText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {

            CollectableItem collectableItem = other.GetComponent<CollectableItem>();
            if (collectableItem != null)
            {
                collectableItem.Collect();
                coins++;
                updateCoinCounterTxt();
            }
           
        }
    }

    public void updateCoinCounterTxt()
    {
        coinText.text = "Coins: " + coins.ToString();
    }
}