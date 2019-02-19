using UnityEngine;
using System.Collections;

public enum EquipmentType
{
    Helmet,
    Weapon1,
    Weapon2,
    Accessory1,
    Accessory2,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Create Item/Equippable Item")]
public class EquippableItem : Item
{
    [Header("Addition Bonus")]
    [Space]
    public int itemDamageReduction;
    public float speedBonus;
    public Vector4 directionJuiceBonus;
    [Space]
    [Header("Percentage Bonus")]
    [Space]
    public int damageReductionPercentage;
    public int speedBonousPercentage;
    public Vector4 directionJuiceBonousPercentage;
    [Space]
    public EquipmentType equipmentType;
}
