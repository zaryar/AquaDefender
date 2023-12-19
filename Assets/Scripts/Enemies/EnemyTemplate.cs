using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Standardwert für maximale Gesundheit
    [SerializeField] private int Health;         // Aktuelle Gesundheit
    [SerializeField] private GameObject HitParticle; // Trefferpartikel
    public HealthBar3D healthbar;               // Referenz auf eine 3D-Gesundheitsleiste, falls verwendet

    private void Awake()
    {
        Health = maxHealth; // Setze die aktuelle Gesundheit auf die maximale Gesundheit
    }

    public void Hurt(int dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            Die();
        }

        if (healthbar != null)
        {
            healthbar.update_healthbar(maxHealth, dmg);
        }
    }

    public int getHealth()
    {
        return Health;
    }

    protected virtual void Die()
    {
        // Logik für den Tod hier einfügen
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null &&
            collision.gameObject.tag == "Bullet" &&
            HitParticle != null)
        {
            Instantiate(HitParticle, collision.transform.position, Quaternion.identity);
        }
    }

    // Methode zum Einstellen der Gesundheit
    public void SetHealth(int health)
    {
        maxHealth = health;
        Health = health;

        // Aktualisiere die Gesundheitsleiste, falls vorhanden
        if (healthbar != null)
        {
            healthbar.update_healthbar(maxHealth, 0); // Setze den Schaden auf 0, da es sich um eine Initialisierung handelt
        }
    }
}
