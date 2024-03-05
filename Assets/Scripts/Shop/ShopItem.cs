using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Scriptable Objects/New Shop Item", order = 1)]

public class ShopItem : ScriptableObject
{
    public string itemName;
    public string itemAmount;
    public int itemCost;
    public Sprite itemIcon;
}
