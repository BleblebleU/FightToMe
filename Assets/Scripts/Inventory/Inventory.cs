using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public CharacterItemsList playerInventoryList;
    public CharacterItemsList displayPlayerInventoryList;

    public Item pickupItem;
    public Item itemToRemove;

    public float inventorySpace;

    public BoolVariable pickedItem;
    public BoolVariable isItemToRemove;

    public BoolVariable inventoryKey;
    [SerializeField] GameObject inventoryUI;

    [SerializeField] Transform itemSlotsParent;
    //[SerializeField] InventorySlot[] inventorySlots;

    public FloatReference numberOfItemsWithPlayer;

    //private void OnValidate()
    //{
    //    if (itemSlotsParent != null)
    //    {
    //        inventorySlots = itemSlotsParent.GetComponentsInChildren<InventorySlot>();
    //    }
    //}

    private void Start()
    {
        pickedItem.boolState = false;
        numberOfItemsWithPlayer.Variable.Value = playerInventoryList.itemsList.Count;
    }

    private void Update()
    {
        if (inventoryKey.boolState == true)
        {
            ActivateInventory();
            inventoryKey.boolState = false;
        }

    }

    private void LateUpdate()
    {
        if (pickedItem.boolState == true && pickupItem != null)
        {
            PickedUpItem(pickupItem);
            pickedItem.boolState = false;
        }
    }

    private void PickedUpItem(Item item)
    {
        if(playerInventoryList.itemsList.Count < inventorySpace)
        {
            AddItem(item);
            //Debug.Log(item.itemName + " added to inventory");
        }
        else
        {
            Debug.Log("Inventory is Full");
            Debug.Log(playerInventoryList.itemsList.Count);
        }
    }

    private void AddItem(Item item)
    {
        if (item != null)
        {
            //int itemIndex = playerInventoryList.itemsList.IndexOf(item);

            //if (playerInventoryList.itemsList.Contains(item) && item.stackable == true)
            //{
            //    //playerInventoryList.itemsList[itemIndex].itemStack++;
            //    playerInventoryList.IncItemStackNumber(item);
            //    displayPlayerInventoryList.itemsList = playerInventoryList.itemsList;
            //    Debug.Log("inc stackable item stack");
            //}
            //else
            //{
            //    playerInventoryList.itemsList.Add(item);
            //    displayPlayerInventoryList.itemsList = playerInventoryList.itemsList;
            //    Debug.Log("added stackable item to stack");
            //}
        }
        else
        {
            Debug.LogWarning("THIS IS A WARNING item IS NULLcant add item");
        }
    }

    private void ActivateInventory()
    {
        if (inventoryKey)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Debug.Log(!inventoryUI.activeSelf);
        }
    }
}
