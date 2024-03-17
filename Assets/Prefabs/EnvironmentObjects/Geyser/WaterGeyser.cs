using UnityEngine;

public class WaterGeyser : MonoBehaviour
{
    public float speed = 14f; // Geschwindigkeit des Projektils
    public float lifetime = 8f;
    private Rigidbody rb;
    public int damage = 1;

    // Konfigurierbare Abweichung
    public float maxDeviationAngle = 5f; // Maximale Abweichung in Grad

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    public void InitializeWater(Vector3 targetPosition)
    {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Zuf?llige Abweichung hinzuf?gen
        direction = Quaternion.Euler(
            Random.Range(-maxDeviationAngle, maxDeviationAngle), // X-Abweichung
            Random.Range(-maxDeviationAngle, maxDeviationAngle), // Y-Abweichung
            Random.Range(-maxDeviationAngle, maxDeviationAngle)  // Z-Abweichung
        ) * direction;

        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // ?berpr?fe, ob das getroffene Objekt der Spieler ist
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage*3);
            Destroy(gameObject);
        }
        if(collision.gameObject.GetComponent<BasicEnemy>())
        {
            collision.gameObject.GetComponent<BasicEnemy>().Hurt(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground") // ?berpr?fe, ob das getroffene Objekt der Boden ist
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag != "WaterDrop")
        {
            Destroy(gameObject);
        }
    }
}
