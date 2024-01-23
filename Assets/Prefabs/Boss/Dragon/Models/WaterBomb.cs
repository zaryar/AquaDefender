using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    public int damage = 10; // Schaden, den die Wasserkugel verursacht

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // Überprüfe, ob das getroffene Objekt der Spieler ist
        {
            // Füge dem Spieler Schaden zu
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);

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