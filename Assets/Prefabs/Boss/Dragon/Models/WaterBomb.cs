using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    public int damage = 10; // Schaden, den die Wasserkugel verursacht

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // �berpr�fe, ob das getroffene Objekt der Spieler ist
        {
            // F�ge dem Spieler Schaden zu
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);

            // Erzeuge Splash-Effekte (falls vorhanden)

            // Zerst�re die Wasserkugel nach der Kollision
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground") // �berpr�fe, ob das getroffene Objekt der Boden ist
        {
            Destroy(gameObject);
        }
        
    }
}