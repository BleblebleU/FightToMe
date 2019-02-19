using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipped Items List", menuName = "Scriptable Objects/Lists/Equipped Items List", order = 2)]
public class EquippedItems : ScriptableObject {

    public List<EquippableItem> equippedItems = new List<EquippableItem>();

    public bool EquipItem(EquippableItem itemToBeEquipped, out Item previousItem)
    {
        if (ContainsSimilarEquipmentType(itemToBeEquipped))
        {
            SwapEquippedItems(itemToBeEquipped, out previousItem);
            return true;
        }
        else
        {
            equippedItems.Add(itemToBeEquipped);
            previousItem = null;
            return true;
        }
    }

    public bool ContainsSimilarEquipmentType(EquippableItem ItemToEquipp)
    {
        for (int i = 0; i < equippedItems.Count; i++)
        {
            if(equippedItems[i].equipmentType == ItemToEquipp.equipmentType)
            {
               return true;
            }
        }
        return false;
    }

    public bool SwapEquippedItems(EquippableItem itemToEquipp, out Item previousEquippedItem)
    {
        for (int i = 0; i < equippedItems.Count; i++)
        {
            if(equippedItems[i].equipmentType == itemToEquipp.equipmentType)
            {
                previousEquippedItem = equippedItems[i];
                equippedItems[i] = itemToEquipp;
                return true;
            }
        }
        previousEquippedItem = null;
        return false;
    }

    public void RemoveItemFromEquippedItems(Item equippedItem)
    {
        for (int i = 0; i < equippedItems.Count; i++)
        {
            if(equippedItems[i].itemNumber == equippedItem.itemNumber)
            {
                equippedItems.RemoveAt(i);
            }
        }
    }

}
