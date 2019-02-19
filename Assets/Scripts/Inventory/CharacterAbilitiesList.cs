using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability List", menuName = "Scriptable Objects/Lists/Ability List", order = 2)]
public class CharacterAbilitiesList : ScriptableObject {

    public BoolVariable playerDash;
    public BoolVariable playerDoubleJump;
    public BoolVariable playerWallClimb;

    public List<AbilityItem> characterAbilities = new List<AbilityItem>();

    public bool AddAndActivateAbility(AbilityItem abilityToAdd)
    {
        bool abilityAdded = false;

        int numberOfCharAbilities = characterAbilities.Count;
        characterAbilities.Add(abilityToAdd);

        if(numberOfCharAbilities + 1 == characterAbilities.Count && ActivateAbility(abilityToAdd))
        {
            abilityAdded = true;
        }

        return abilityAdded;
    }

    private bool ActivateAbility(AbilityItem ability)
    {

        if (ability.abilityType == AbilityType.Dash)
        {
            playerDash.boolState = true;
            return true;
        }
        else if (ability.abilityType == AbilityType.DoubleJump)
        {
            playerDoubleJump.boolState = true;
            return true;
        }
        else if (ability.abilityType == AbilityType.WallClimb)
        {
            playerWallClimb.boolState = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
