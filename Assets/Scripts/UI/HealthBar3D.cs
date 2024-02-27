using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar3D : MonoBehaviour
{
    Transform _barLife;
    Transform _barBG;
    private float startSizeHealthBar = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("HealthBar_Life").GetComponent<Renderer>().material.color = Color.green;
        gameObject.transform.Find("HealthBar_Background").GetComponent<Renderer>().material.color = Color.red;
        _barLife = gameObject.transform.Find("HealthBar_Life");
        _barBG = gameObject.transform.Find("HealthBar_Background");
        startSizeHealthBar = _barLife.localScale.x;
    }

    // Update is called once per frame
    public void adapt_bar(Transform barobject, float substract)
    {
        barobject.localPosition = new Vector3(barobject.localPosition.x - (0.5f * System.Math.Abs(substract)), barobject.localPosition.y, 0);
        barobject.localScale = new Vector3(Mathf.Max(0,barobject.localScale.x + substract), barobject.localScale.y, barobject.localScale.z);
    }

    public void update_healthbar(int maxHealth, int dmg)
    {
        float substract = ((float)dmg / (float)maxHealth)* startSizeHealthBar;
        adapt_bar(_barLife, -substract);
        adapt_bar(_barBG, substract);
    }
}
