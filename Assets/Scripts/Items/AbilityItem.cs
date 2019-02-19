using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityType
{
    Dash,
    DoubleJump,
    WallClimb
}

[CreateAssetMenu(fileName = "Ability Item", menuName = "Scriptable Objects/Create Item/Ability Item")]
public class AbilityItem : ScriptableObject {

    public AbilityType abilityType;
    public Sprite abilityIcon;
    public string abilityDescription;
}
