using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CoinCounter : MonoBehaviour
{
    public int coinCount = 0;
    public AudioClip coinSound;
    //public Text coinText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            Destroy(other.gameObject);
            coinCount++;
            //coinText.text = "Coins: " + coinCount.ToString();
        }
    }
}