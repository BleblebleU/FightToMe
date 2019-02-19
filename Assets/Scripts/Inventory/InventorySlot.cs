using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;

    public CharacterItemsList playerInventory;
    public EquippedItems playerEquippedItems;
    public ItemsToConsumeList playerItemsToConsume;

    public BoolVariable equippedItemsChanged;

    public Text itemStackAmount;

    Item item;
    Item previousItem;

    internal bool slotIsFull = false;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.itemIcon;
        icon.enabled = true;
        removeButton.interactable = true;
        if(item.stackable == true && playerInventory.ItemAndStackNumber(item).y > 1)
        {
            itemStackAmount.text = playerInventory.ItemAndStackNumber(item).y.ToString();
            itemStackAmount.enabled = true;
        }
        else
        {
            itemStackAmount.enabled = false;
        }
    }

    public void OnItemRemove()
    {
        if (item.stackable == true)
        {
            if (playerInventory.ItemAndStackNumber(item).y == 1)
            {
                playerInventory.itemsList.Remove(item);
                playerInventory.RemoveItemFromStack(item);
            }
            else
            {
                playerInventory.DecItemStackNumber(item);
            }

        }
        else
        {
            playerInventory.itemsList.Remove(item);
            playerInventory.RemoveItemFromStack(item);
        }

        ClearSlot();
    }

    public void OnUseItem()
    {
        if (item is EquippableItem)
        {
            playerEquippedItems.EquipItem((EquippableItem)item, out previousItem);
            if (previousItem != null)
            {
                playerInventory.itemsList.Add(previousItem);
            }

            OnItemRemove();
            equippedItemsChanged.boolState = true;
        }
        else if (item is ConsummableItem)
        {
            //CHANGE THIS TO CONSUME ITEM OR THIS IS WHY ITEM WILL GET DELETED
            if (playerItemsToConsume.ConsumeItem((ConsummableItem)item))
            {
                OnItemRemove();
            }
            else
            {
                Debug.Log("Item effect cannot eb applied here");
            }
        }
        else
        {
            Debug.Log("Wth is this item");
        }
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        itemStackAmount.enabled = false;
    }

}
