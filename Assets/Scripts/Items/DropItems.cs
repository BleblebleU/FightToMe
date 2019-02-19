using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour {

    [SerializeField] private int numberOfDropItems;
    [SerializeField] private Item[] itemsToDrop  = new Item[4];

    public BoolVariable dropItem;

    private void DropItem()
    {
        if(dropItem.boolState == true)
        {
            int randomItemIndex = Random.Range(0, numberOfDropItems - 1);
            Instantiate(itemsToDrop[randomItemIndex], transform);
            dropItem.boolState = false;
        }
    }
}
