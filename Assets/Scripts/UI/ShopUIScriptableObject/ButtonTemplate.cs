using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonTemplate : MonoBehaviour
{
    public Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public Text priceText;
    public Text stackItem;

    internal bool itemIsStackable;
    private float price;

    public Item itemToSell;
    public Item duplicateItem;

    internal CharacterItemScrollList scrollList;

    public ButtonTemplate thisButtom;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
        itemIsStackable = false;
        stackItem.enabled = false;
    }

    public void SetupItem(Item currentItem, CharacterItemScrollList currentShop)
    {
        duplicateItem = currentItem;
        float itemStackAmount = currentShop.shopList.ItemAndStackNumber(duplicateItem).y;
        if (duplicateItem.stackable == true && itemStackAmount > 0)
        {
            itemIsStackable = true;
            stackItem.text = itemStackAmount.ToString();
            stackItem.enabled = true;
        }
        else
        {
            itemIsStackable = false;
            stackItem.enabled = false;
        }
        nameLabel.text = duplicateItem.itemName;
        iconImage.sprite = duplicateItem.itemIcon;
        priceText.text = duplicateItem.itemCost.ToString();
        price = duplicateItem.itemCost;
        
        //duplicateItem.currentShopIndex.Variable.Value = currentShop.shopList.shopReferenceNumber.Value;
        scrollList = currentShop;
    }


    public void SetItemToBeSold(Item item)
    {
        item = duplicateItem;
        item.name = duplicateItem.itemName;
        item.itemIcon = duplicateItem.itemIcon;
        item.itemCost = duplicateItem.itemCost;
        //item.currentShopIndex.Variable.Value = duplicateItem.currentShopIndex.Value;
        //Debug.Log("ItemSet");
    }

    public void HandleClick()
    {
        SetItemToBeSold(itemToSell);

        ////Debug.Log(thisButtom.duplicateItem.itemName);
        scrollList.TryTransferItemToOtherShop(duplicateItem);
    }
}