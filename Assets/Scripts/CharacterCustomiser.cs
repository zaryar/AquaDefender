using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomiser : MonoBehaviour
{
    [SerializeField] SwordModelSwapper sms;
    Weapons.Swords sword = Weapons.Swords.sword1;
    public void nextSword()
    {
        Weapons.Swords next;
        if(sword == Weapons.Swords.sword1)
        {
            next = Weapons.Swords.sword3;
        }
        else if (sword == Weapons.Swords.sword2)
        {
            next = Weapons.Swords.sword1;
        }
        else if (sword == Weapons.Swords.sword3)
        {
            next = Weapons.Swords.sword2;
        }
        else
        {
            next = Weapons.Swords.sword1;
        }
        sms.swapModel(next);
        sword = next;
    }

    private void Start()
    {
        sms.enableRenderer();
        sms.swapModel(sword);
    }
}
