using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New ConsumeItem", menuName = "Scriptable Objects/Lists/ConsumeItems")]
public class ItemsToConsumeList : ScriptableObject
{
   //public List<ConsummableItem> itemsToConsume = new List<ConsummableItem>();

    public FloatReference playerHealth;
    public FloatReference playerSpeed;
    public FloatReference playerLeftRemainingMoves;
    public FloatReference playerRightRemainingMoves;
    public FloatReference playerJumpRemainingMoves;
    public FloatReference playerDownRemainingMoves;

    public bool ConsumeItem(ConsummableItem itemToConsume)
    {
        if(itemToConsume.effectType == EffectType.InstantEffect)
        {
            if(itemToConsume.comsumableType == ConsumableTypes.healthRelated)
            {
                if(playerHealth.Variable.Value < 10)
                {
                    playerHealth.Variable.Value += itemToConsume.bonousHealth;
                    //Debug.Log(playerHealth.Variable.Value);
                    return true;
                }
            }
            else if(itemToConsume.comsumableType == ConsumableTypes.LeftMovementRelated || itemToConsume.comsumableType == ConsumableTypes.RightMovementRelated || itemToConsume.comsumableType == ConsumableTypes.JumpMovementRelated || itemToConsume.comsumableType == ConsumableTypes.DownMovementRelated)
            {
                if(itemToConsume.comsumableType == ConsumableTypes.LeftMovementRelated && playerLeftRemainingMoves.Variable.Value < 20)
                {
                    playerLeftRemainingMoves.Variable.Value += itemToConsume.directionJuiceBonus.x;
                    return true;
                }
                else if(itemToConsume.comsumableType == ConsumableTypes.RightMovementRelated && playerRightRemainingMoves.Variable.Value < 20)
                {
                    playerRightRemainingMoves.Variable.Value += itemToConsume.directionJuiceBonus.y;
                    return true;
                }
                else if (itemToConsume.comsumableType == ConsumableTypes.JumpMovementRelated && playerJumpRemainingMoves.Variable.Value < 20)
                {
                    playerJumpRemainingMoves.Variable.Value += itemToConsume.directionJuiceBonus.z;
                    return true;
                }
                else if (itemToConsume.comsumableType == ConsumableTypes.DownMovementRelated && playerDownRemainingMoves.Variable.Value < 20)
                {
                    playerDownRemainingMoves.Variable.Value += itemToConsume.directionJuiceBonus.w;
                    return true;
                }
            }
        }
        return false;
    }


}
