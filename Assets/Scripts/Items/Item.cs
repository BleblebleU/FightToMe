using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Create Item/Generic Item", order = 0)]
public class Item : ScriptableObject {

    [Header("Item Data")]
    [Space]
    public string itemName;
    public float itemCost;
    public Sprite itemIcon;
    public string itemDescription;

    public int itemNumber;
    public bool stackable;
}
