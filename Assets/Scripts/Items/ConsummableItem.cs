using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableTypes
{
    healthRelated,
    LeftMovementRelated,
    RightMovementRelated,
    JumpMovementRelated,
    DownMovementRelated,
    SpeedRelated,
    AttackRelated
}

public enum EffectType
{
    TimeEffect,
    InstantEffect
}
[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Create Item/Consumable Item")]
public class ConsummableItem : Item {

    public int bonousHealth;
    [Header("Addition Bonus")]
    [Space]
    public int DamageReduction;
    public float speedBonus;
    public Vector4 directionJuiceBonus;
    [Space]
    [Header("Percentage Bonus")]
    [Space]
    public int damageReductionPercentage;
    public int speedBonousPercentage;
    public Vector4 directionJuiceBonousPercentage;
    [Space]
    public ConsumableTypes comsumableType;
    public EffectType effectType;
}
