using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryAndStats : MonoBehaviour {

    public CharacterItemsList playerInventoryList;

    public Item pickupItem;
    public Item itemToRemove;

    public float inventorySpace;

    public BoolVariable pickedItem;
    public BoolVariable isItemToRemove;

    public BoolVariable inventoryKey;
    [SerializeField] GameObject inventoryUI;

    [SerializeField] Transform itemSlotsParent;
    [SerializeField] InventorySlot[] inventorySlots;

    public FloatReference numberOfItemsWithPlayer;

    private void OnValidate()
    {
        if (itemSlotsParent != null)
        {
            inventorySlots = itemSlotsParent.GetComponentsInChildren<InventorySlot>();
        }
    }

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

    private void ActivateInventory()
    {
        if (inventoryKey)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            //Debug.Log(!inventoryUI.activeSelf);
        }
    }
}
