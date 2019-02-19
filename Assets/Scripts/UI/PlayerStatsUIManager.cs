using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUIManager : MonoBehaviour {

    public FloatReference damageReduction;
    public FloatReference attackStrength;

    [SerializeField] TextMeshProUGUI damageReductionStatText;
    [SerializeField] TextMeshProUGUI attackStrengthStatText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetDamageReductionText(damageReduction);
        SetAttackText(attackStrength);
	}

    private void SetDamageReductionText(FloatReference damageReduction)
    {
        damageReductionStatText.SetText(damageReduction.Variable.Value.ToString());
    }

    private void SetAttackText(FloatReference attackStrength)
    {
        attackStrengthStatText.SetText(attackStrength.Variable.Value.ToString());
    }


}
