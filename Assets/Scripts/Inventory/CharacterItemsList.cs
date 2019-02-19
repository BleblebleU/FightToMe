using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory List", menuName = "Scriptable Objects/Lists/Inventory List", order = 2)]
public class CharacterItemsList : ScriptableObject {

    public FloatReference gold;
    public FloatReference shopReferenceNumber;

    public const int characterInventoryLimit = 20;

    public List<Item> itemsList = new List<Item>();
    public Vector2[] itemAndStackNumber = new Vector2[characterInventoryLimit];

    public AllItems itemsRef;
    public Vector2[] loadedItemNumbers = new Vector2[characterInventoryLimit];

    public void LoadingItems()
    {
        Item[] itemsToLoad = new Item[loadedItemNumbers.Length];
        for (int i = 0; i < loadedItemNumbers.Length; i++)
        {
            itemsToLoad[i] = itemsRef.ItemWith((int)loadedItemNumbers[i].x);
        }

        itemsList.Clear();
        for (int i = 0; i < itemsToLoad.Length; i++)
        {
            if(itemsToLoad[i] != null)
            {
                itemsList.Add(itemsToLoad[i]);
            }
        }

        for (int i = 0; i < loadedItemNumbers.Length; i++)
        {
            itemAndStackNumber[i].x = loadedItemNumbers[i].x;
            itemAndStackNumber[i].y = loadedItemNumbers[i].y;
        }

    }

    public bool PickUpItemHandler(Item item, bool remove)
    {
        if (item.stackable == true && ContainsItemInStack(item))
        {
            if (remove == false)
            {
                IncItemStackNumber(item);
                return true;
            }
            else
            {
                if(ItemAndStackNumber(item).y > 1)
                {
                    DecItemStackNumber(item);
                    return true;
                }
                else
                {
                    RemoveItemFromStack(item);
                    return true;
                }
            }
            
        }
        else
        {
            if(itemsList.Count < characterInventoryLimit)
            {
                itemsList.Add(item);
                AddItemToStack(item);
                return true;
            }
            else
            {
                //Debug.Log("Could not pickup: " + item.itemName + " because no more space in inventory");
                return false;
            }
        }
    }

    public void ClearAndAddItemData()
    {
        ClearItemAndStackRef();
        AddItemFromItemList();
    }

    private void ClearItemAndStackRef()
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            itemAndStackNumber[i] = Vector2.zero;
        }
    }

    private void AddItemFromItemList()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            itemAndStackNumber[i] = new Vector2(itemsList[i].itemNumber, 1);
        }
    }

    public Vector2 ItemAndStackNumber(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                return itemAndStackNumber[i];
            }
        }
        return Vector2.zero;
    }

    public bool ContainsItemInStack(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddItemToStack(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if(itemAndStackNumber[i].x == 0)
            {
                itemAndStackNumber[i].x = item.itemNumber;
                itemAndStackNumber[i].y = 1;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItemFromStack(Item item)
    {
        for (int i = itemAndStackNumber.Length - 1; i >= 0; i--)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                itemAndStackNumber[i] = Vector2.zero;
                return true;
            }
        }
        return false;
    }

    public bool IncItemStackNumber(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if(itemAndStackNumber[i].x == item.itemNumber)
            {
               itemAndStackNumber[i].y++;
                return true;
            }
        }
        return false;
    }

    public bool DecItemStackNumber(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                itemAndStackNumber[i].y--;
                return true;
            }
        }
        return false;
    }
}
