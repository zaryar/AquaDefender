using UnityEngine;

public class ChestWater : MonoBehaviour
{
    public float speed = 14f; // Geschwindigkeit des Projektils
    public float lifetime = 8f;
    public int damage = 1;

    // Konfigurierbare Abweichung
    public float maxDeviationAngle = 5f; // Maximale Abweichung in Grad

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void InitializeWater(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Zuf?llige Abweichung hinzuf?gen
        direction = Quaternion.Euler(
            Random.Range(-maxDeviationAngle, maxDeviationAngle), // X-Abweichung
            Random.Range(-maxDeviationAngle, maxDeviationAngle), // Y-Abweichung
            Random.Range(-maxDeviationAngle, maxDeviationAngle)  // Z-Abweichung
        ) * direction;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            // Spieler Schaden zufügen
            other.gameObject.GetComponent<Health>().TakeDamage(damage * 3);
            Destroy(gameObject);
            Debug.Log("Aua");
        }
        
    }
}
