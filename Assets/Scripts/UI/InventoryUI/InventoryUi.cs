using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour {

    public CharacterItemsList playerInventoryList;
    public FloatReference numberOfItemsWithPlayer;

    [SerializeField] BoolVariable[] interactedWithItem = new BoolVariable[3];

    [SerializeField] Transform itemsParent;
    InventorySlot[] slots;

    private float itemProduct;
    private bool stackItemExists;

    private void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        UpdateInventoryUI();
    }

    private void Update()
    {
        for (int i = 0; i < interactedWithItem.Length; i++)
        {
            if (interactedWithItem[i].boolState == true)
            {
               UpdateInventoryUI();
            }
        }
       // Debug.Log(interactedWithItem[1]);
    }


    private void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < playerInventoryList.itemsList.Count)
            {
                slots[i].AddItem(playerInventoryList.itemsList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
        
}
