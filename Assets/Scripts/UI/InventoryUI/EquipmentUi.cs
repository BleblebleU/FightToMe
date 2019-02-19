using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUi : MonoBehaviour {

    public EquippedItems playerEquippedItems;

    public BoolVariable equippedItemsChanged;

    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;


    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    private void Start()
    {
        RefreshEquippedItems();
    }

    private void LateUpdate()
    {
        if(equippedItemsChanged.boolState == true)
        {
            RefreshEquippedItems();
            equippedItemsChanged.boolState = false;
        }
    }

    public void RefreshEquippedItems()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            for (int y = 0; y < playerEquippedItems.equippedItems.Count; y++)
            {
                if (equipmentSlots[i].equipmentType == playerEquippedItems.equippedItems[y].equipmentType)
                {
                    equipmentSlots[i].equipItemToSlot(playerEquippedItems.equippedItems[y]);
                    equipmentSlots[i].slotIsFull = true;
                    //Debug.Log(equipmentSlots[i].slotIsFull);
                }
            }
            //if(i >= playerEquippedItems.equippedItems.Count)
            //{
            //    equipmentSlots[i].ResetSlot();
            //}
        }
    }

}
