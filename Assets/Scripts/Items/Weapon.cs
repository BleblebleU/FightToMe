using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    Square,
    Circle
}

[CreateAssetMenu(fileName = " New Weapon", menuName = "Scriptable Objects/weapon")]
public class Weapon : EquippableItem {

    public FloatReference Damage;
    private ColliderType colliderType;

    public FloatReference AttackRangeX;
    public FloatReference AttackRangeY;
    public FloatReference AttackRadius;

    public float angleOfBox;

    public FloatReference timeBtwAttack;

    [SerializeField] ColliderType weaponColType;

    public ColliderType ColliderType
    {
        get
        {
            return weaponColType;
        }

        set
        {
            weaponColType = value;
            CompleteWeapon();
        }
    }

    public void CompleteWeapon()
    {
        if (weaponColType == ColliderType.Square)
        {
            AttackRadius.UseConstant = true;
            AttackRadius.ConstantValue = 0;
        }
        else if (weaponColType == ColliderType.Circle)
        {
            AttackRangeX.UseConstant = true;
            AttackRangeX.ConstantValue = 0;
            AttackRangeY.UseConstant = true;
            AttackRangeY.ConstantValue = 0;
        }
    }
}

