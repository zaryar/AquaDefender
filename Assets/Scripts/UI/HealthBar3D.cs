using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("HealthBar_Life").GetComponent<Renderer>().material.color = Color.green;
        gameObject.transform.Find("HealthBar_Background").GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    public void adapt_bar(string bar, float substract)
    {
        Transform barobject = gameObject.transform.Find(bar);
        float x_position = barobject.transform.localPosition.x;
        float x_scale = barobject.transform.localScale.x;
        barobject.transform.localPosition = new Vector3(x_position - (0.5f * System.Math.Abs(substract)), 1.2f, 0);
        barobject.transform.localScale = new Vector3(x_scale + substract, 0.2f, 0.2f);
    }

    public void update_healthbar(int maxHealth, int dmg)
    {
        float substract = (float)dmg / (float)maxHealth;
        adapt_bar("HealthBar_Life", -substract);
        adapt_bar("HealthBar_Background", substract);
    }
}
