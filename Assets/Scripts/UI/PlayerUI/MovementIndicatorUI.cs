using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementIndicatorUI : MonoBehaviour {

    [SerializeField] GameObject movementIndicatorsParent;

    [SerializeField] Image[] movementIndicators;

    public FloatReference[] currentMoves;
    [SerializeField] float[] totalCurrentMoves;

    //private void OnValidate()
    //{
    //    if(movementIndicatorsParent != null)
    //    {
    //        movementIndicators = movementIndicatorsParent.GetComponentsInChildren<Image>();
    //    }
    //}

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < movementIndicators.Length; i++)
        {
            movementIndicators[i].fillAmount = currentMoves[i].Value / totalCurrentMoves[i];
        }
    }
}
