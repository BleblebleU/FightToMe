using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value
    {
        get
        {
            if (UseConstant)
            {
                return ConstantValue;
            }
            else
            {
                return Variable.Value;
            }
        }
    }

}

[CreateAssetMenu(fileName = " New Float", menuName = "Scriptable Objects/Variables/Float", order = 0)]
public class FloatVariable : ScriptableObject
{
    public float Value;
}
