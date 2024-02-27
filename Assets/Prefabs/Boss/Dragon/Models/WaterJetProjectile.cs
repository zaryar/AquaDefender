using UnityEngine;

public class WaterJetProjectile : MonoBehaviour
{
    public float speed = 10f; // Geschwindigkeit des Projektils
    public float lifetime = 3f;
    private Rigidbody rb;

    // Konfigurierbare Abweichung
    public float maxDeviationAngle = 5f; // Maximale Abweichung in Grad

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    public void InitializeProjectile(Vector3 targetPosition)
    {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Zufällige Abweichung hinzufügen
        direction = Quaternion.Euler(
            Random.Range(-maxDeviationAngle, maxDeviationAngle), // X-Abweichung
            Random.Range(-maxDeviationAngle, maxDeviationAngle), // Y-Abweichung
            Random.Range(-maxDeviationAngle, maxDeviationAngle)  // Z-Abweichung
        ) * direction;

        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // Überprüfe, ob das getroffene Objekt der Spieler ist
        {
            // Füge dem Spieler Schaden zu
            collision.gameObject.GetComponent<Health>().TakeDamage(1);

            // Erzeuge Splash-Effekte (falls vorhanden)

            // Zerstöre die Wasserkugel nach der Kollision
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground") // Überprüfe, ob das getroffene Objekt der Boden ist
        {
            Destroy(gameObject);
        }
    }
}
