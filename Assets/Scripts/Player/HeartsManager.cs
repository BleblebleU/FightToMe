using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour {

    public FloatReference playerHealth;
    [SerializeField]private int numberOfHeartsVisible;

    public Transform heartsParent;
    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void OnValidate()
    {
        //hearts = heartsParent.GetComponentsInChildren<Image>();
        //UpdateHeartsUI();
    }

    private void Start()
    {
        hearts = heartsParent.GetComponentsInChildren<Image>();
        UpdateHeartsUI();
    }

    private void Update()
    {
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        if(playerHealth != null)
        {
            if (playerHealth.Variable.Value > numberOfHeartsVisible)
            {
                playerHealth.Variable.Value = numberOfHeartsVisible;
            }
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < playerHealth.Variable.Value)
                {
                    hearts[i].sprite = fullHeart;
                    hearts[i].color = Color.white;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                    hearts[i].color = Color.grey;
                }
                if (i < numberOfHeartsVisible)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
    }

}
