using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscapeShip : MonoBehaviour
{
    private int currentShipLvl = 0;

    public void increaseShipLvl()
    {
        if (currentShipLvl < 6) {
            Debug.Log("+1 schiffs Teil");
            currentShipLvl++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
