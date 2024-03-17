using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Standardwert f�r maximale Gesundheit
    private int Health; // Aktuelle Gesundheit
    [SerializeField] float deathTimer = 0f;
    [SerializeField] private GameObject HitParticle; // Trefferpartikel
    public HealthBar3D healthbar;               // Referenz auf eine 3D-Gesundheitsleiste, falls verwendet
    protected bool _isDead = false;
    public bool hurt = false;
    private void Awake()
    {
        Health = maxHealth; // Setze die aktuelle Gesundheit auf die maximale Gesundheit
    }

    public void Hurt(int dmg)
    {
        Health -= dmg;
        hurt = true;
        if (Health <= 0)
        {
            if(healthbar != null && !_isDead)
            {
                healthbar.update_healthbar(maxHealth, dmg + Health);
            }
            Die();
        }

        else if (healthbar != null)
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
        // Logik f�r den Tod hier einf�gen
        _isDead= true;
        Destroy(gameObject, deathTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag != "WaterDrop" &&
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

    public void HealthInit(){
        Health = maxHealth;
    }
}
