using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItemScrollList : MonoBehaviour {

    public BoolVariable shopedItem;

    public CharacterItemsList shopList;
    public Item item;

    public CharacterItemsList otherShopList;
    public CharacterItemScrollList otherShopScrollScript;

    public Transform contentPanel;
    public Text myGoldDisplay;
    public SimpleObjectPool buttonObjectPool;

    private bool canTransfer = true;

    // Use this for initialization

    private void OnEnable()
    {
        RefreshDisplay();
    }

    void Start()
    {
        RefreshDisplay();
        //Debug.Log(shopList.gold.Value);
    }

    void RefreshDisplay()
    {
        myGoldDisplay.text = "Gold: " + shopList.gold.Value.ToString();
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
            //Debug.Log("button removed");
        }

    }

    private void AddButtons()
    {
        for (int i = 0; i < shopList.itemsList.Count; i++)
        {
            Item item = shopList.itemsList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);
            newButton.transform.localScale = new Vector3(1, 1, 1);

            ButtonTemplate buttonTemplate = newButton.GetComponent<ButtonTemplate>();
            buttonTemplate.SetupItem(item, this);
        }
    }

    public void TryTransferItemToOtherShop(Item item)
    {
        if (otherShopList.gold.Value >= item.itemCost && canTransfer)
        {
            shopList.gold.Variable.Value += item.itemCost;
            otherShopList.gold.Variable.Value -= item.itemCost;
            //otherShopScrollScript.shopList.gold.Variable.Value -= item.itemCost;

            AddItem(item, otherShopList);
            RemoveItem(item, shopList);

            shopedItem.boolState = true;

            RefreshDisplay();
            otherShopScrollScript.RefreshDisplay();
            //Debug.Log("enough gold");

        }
        else
        {
            //Debug.Log("attempted");
            shopedItem.boolState = false;
        }
        
    }

    void AddItem(Item itemToAdd, CharacterItemsList shopList)
    {
        //Debug.Log(itemToAdd.name + "added");

        if (shopList.itemsList.Contains(itemToAdd) && itemToAdd.stackable == true)
        {
            if (shopList.ContainsItemInStack(itemToAdd))
            {
                shopList.IncItemStackNumber(itemToAdd);
            }
            else
            {
                shopList.AddItemToStack(itemToAdd);
                shopList.IncItemStackNumber(itemToAdd);
            }
        }
        else
        {
            shopList.itemsList.Add(itemToAdd);
            shopList.AddItemToStack(itemToAdd);
        }
    }

    private void RemoveItem(Item itemToRemove, CharacterItemsList shopList)
    {
        //Debug.Log(itemToRemove.name + "removed");

        for (int i = shopList.itemsList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemsList[i] == itemToRemove)
            {
                if (itemToRemove.stackable == true)
                {
                    if (shopList.ItemAndStackNumber(itemToRemove).y > 1)
                    {
                        shopList.DecItemStackNumber(itemToRemove);
                    }
                    else
                    {
                        shopList.RemoveItemFromStack(itemToRemove);
                        shopList.itemsList.RemoveAt(i);
                    }
                }
                else
                {
                    shopList.RemoveItemFromStack(itemToRemove);
                    shopList.itemsList.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public void BuyOrSellButton()
    {
        //TryTransferItemToOtherShop(itemToSell);
        canTransfer = true;
    }
}
