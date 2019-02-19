using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPalace : MonoBehaviour {

	public BoolVariable accessMemory;
    public GameObject memoryPanel;

    public BoolVariable canOpenMemoryPannel;

	// Use this for initialization
	void Start () {
        canOpenMemoryPannel.boolState = false;
	}

    void LateUpdate () {
        if (canOpenMemoryPannel.boolState == true && accessMemory.boolState == true)
        {
            memoryPanel.SetActive(!memoryPanel.activeSelf);
            accessMemory.boolState = false;
        }
	}
}
