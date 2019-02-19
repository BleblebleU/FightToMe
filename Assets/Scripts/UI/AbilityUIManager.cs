using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUIManager : MonoBehaviour {

    public GameObject abilityUI;

    public void OnOkButton()
    {
        abilityUI.SetActive(false);
        Time.timeScale = 1;
    }
}
