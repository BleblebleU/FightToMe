using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour {

    public Image icon;

    public CharacterItemsList playerInventory;
    public EquippedItems playerEquippedItems;

    public BoolVariable equippedItemsChanged;
    public BoolVariable characterInventoryChanged;
    //public FloatReference itemToUnEquippNumber;

    Item equippedItem;

    public bool slotIsFull;

    public EquipmentType equipmentType;


    private void OnValidate()
    {
        gameObject.name = equipmentType.ToString() + " Slot";
        icon.enabled = false;
    }

    public void equipItemToSlot(EquippableItem equippableItem)
    {
        equippedItem = equippableItem;
        icon.sprite = equippedItem.itemIcon;
        icon.enabled = true;
    }

    public void UnEquipItem()
    {
        playerEquippedItems.RemoveItemFromEquippedItems(equippedItem);

        if (equippedItem.stackable == true)
        {
            if (playerInventory.ContainsItemInStack(equippedItem))
            {
                playerInventory.IncItemStackNumber(equippedItem);
                characterInventoryChanged.boolState = true;
            }
            else
            {
                playerInventory.itemsList.Add(equippedItem);
                characterInventoryChanged.boolState = true;
            }
        }
        else
        {
            playerInventory.itemsList.Add(equippedItem);
            characterInventoryChanged.boolState = true;
        }

        ResetSlot();
        playerEquippedItems.equippedItems.Remove((EquippableItem)equippedItem);
        equippedItemsChanged.boolState = true;
    }

    public void ResetSlot()
    {
        equippedItem = null;
        slotIsFull = false;
        icon.sprite = null;
        icon.enabled = false;
    }
}
