using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour {

    [SerializeField] FloatReference playerMoney;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Debug.Log("Coin Picked UP");
            playerMoney.Variable.Value++;
            Destroy(gameObject);
        }
    }
}
