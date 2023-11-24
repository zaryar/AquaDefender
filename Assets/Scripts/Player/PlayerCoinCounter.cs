using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CoinCounter : MonoBehaviour
{
    public int coinCount = 0;
    //public Text coinText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinCount++;
            //coinText.text = "Coins: " + coinCount.ToString();
        }
    }
}