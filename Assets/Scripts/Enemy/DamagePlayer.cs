using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public FloatReference playerHealth;
    public FloatReference damageReduction;
    [SerializeField] EnemyProperties propertiesOfThisEnemy;

    private void Start()
    {
        propertiesOfThisEnemy = GetComponent<EnemyProperties>();
    }

    private float GetDamage(FloatReference damageReduction, float damage)
    {
        return damage - damage * damageReduction.Variable.Value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth.Variable.Value -= GetDamage(damageReduction, propertiesOfThisEnemy.damageAmount);
        }
    }

}
