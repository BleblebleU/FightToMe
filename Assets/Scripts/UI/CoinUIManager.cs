using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUIManager : MonoBehaviour {

    public TextMeshProUGUI coinsGUIText;
    public FloatReference playerMoney;

    private void OnValidate()
    {
        if(playerMoney != null)
        {
            CoinUIUpdater(playerMoney);
        }
    }

    // Update is called once per frame
    void Update () {
        CoinUIUpdater(playerMoney);
	}

    private void CoinUIUpdater(FloatReference playerMoney)
    {
        coinsGUIText.text = playerMoney.Variable.Value.ToString();
    }

}
