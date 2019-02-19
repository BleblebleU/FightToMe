using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class spikes : MonoBehaviour {

    [Header("Player Variables")]
    public FloatReference playerHealth;
    public BoolVariable playerHealthChanged;
    public FloatReference playerDamageReduction;
    [Space]
    [Header("Enemy Properties")]
    public float damageAmount;

    private BoxCollider2D spikeCollider2d;

    private void Start()
    {
        spikeCollider2d = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Debug.Log("Collided with player reducing Player Health");
            playerHealth.Variable.Value -= damageAmount - playerDamageReduction.Variable.Value * damageAmount;
            //playerHealthChanged.boolState = true;
        }
    }
}
