using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "All Items", menuName = "Scriptable Objects/Create All Items/All Items")]
public class AllItems : ScriptableObject {

    public List<Item> allItems = new List<Item>();

    public Item ItemWith(int itemNumber)
    {
        Item returnItem = null;
        for (int i = 0; i < allItems.Count; i++)
        {
            if(itemNumber != 0 && allItems[i].itemNumber == itemNumber)
            {
                returnItem = allItems[i];
                break;
            }
        }
        return returnItem;
    }
}
