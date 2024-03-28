using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Prüft, ob das Objekt, das den Trigger berührt, mit dem Tag "Player" versehen ist
        {
            Debug.Log("Player hat den Trigger berührt!");
            player.GetComponent<Health>().TakeDamage(5);
        }
    }
}