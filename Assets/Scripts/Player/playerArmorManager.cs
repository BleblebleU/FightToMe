using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerArmorManager : MonoBehaviour {

    public BoolVariable equippedItemsListChanged;
    public FloatReference damageReduction;
    public EquippedItems playerEquippedItemsList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetDamageReduction(damageReduction, playerEquippedItemsList);
    }

    private void SetDamageReduction(FloatReference damageReduction, EquippedItems playerEquippedItems)
    {
        float totalDamageReduction = 0;
        for (int i = 0; i < playerEquippedItems.equippedItems.Count; i++)
        {
            //Debug.Log("Checking equipped items for damage reduction items");
            float damageReductionFromAccessory1 = 0;
            float damageReductionFromAccessory2 = 0;
            float damageReductionFromHelmet = 0;
            float damageReductionFromWeapon2 = 0;

            if (playerEquippedItems.equippedItems[i].equipmentType == EquipmentType.Accessory1)
            {
                //Debug.Log("Caluclated damage reduction of " + playerEquippedItemsList.equippedItems[i].itemName);
                damageReductionFromAccessory1 = playerEquippedItems.equippedItems[i].itemDamageReduction;
            }
            else if (playerEquippedItems.equippedItems[i].equipmentType == EquipmentType.Accessory2)
            {
                //Debug.Log("Caluclated damage reduction of " + playerEquippedItemsList.equippedItems[i].itemName);
                damageReductionFromAccessory2 = playerEquippedItems.equippedItems[i].itemDamageReduction;
            }
            else if (playerEquippedItems.equippedItems[i].equipmentType == EquipmentType.Helmet)
            {
                //Debug.Log("Caluclated damage reduction of " + playerEquippedItemsList.equippedItems[i].itemName);
                damageReductionFromHelmet = playerEquippedItems.equippedItems[i].itemDamageReduction;
            }
            else if (playerEquippedItems.equippedItems[i].equipmentType == EquipmentType.Weapon2)
            {
                //Debug.Log("Caluclated damage reduction of" + playerEquippedItemsList.equippedItems[i].itemName);
                damageReductionFromWeapon2 = playerEquippedItems.equippedItems[i].itemDamageReduction;
            }
            else
            {
                //Debug.Log(playerEquippedItemsList.equippedItems[i].itemName + " does not reduces Damage");
                damageReductionFromAccessory1 = 0;
                damageReductionFromAccessory2 = 0;
                damageReductionFromHelmet = 0;
                damageReductionFromWeapon2 = 0;
            }
            totalDamageReduction += damageReductionFromAccessory1 + damageReductionFromAccessory2 + damageReductionFromHelmet + damageReductionFromWeapon2;
        }

        damageReduction.Variable.Value = totalDamageReduction;
    }
}
