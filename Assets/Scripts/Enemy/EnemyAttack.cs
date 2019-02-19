using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public Weapon weaponType;

    private float weaponDmg;
    private float timeBetweenAttack;
    private float startTimeBtwAttack;
    private ColliderType colType;

    private float boxAngle;

    private float attackRangeX;
    private float attackRangeY;
    private float attackRadius;

    public Transform playerPos;
    public Transform attackPos;

    public LayerMask whatIsEnemy;

    public FloatReference playerHealth;

    void Start() {
        startTimeBtwAttack = weaponType.timeBtwAttack.Value;
        weaponDmg = weaponType.Damage.Value;
        colType = weaponType.ColliderType;
        if(colType == ColliderType.Square) {
            attackRangeX = weaponType.AttackRangeX.Value;
            attackRangeY = weaponType.AttackRangeY.Value;
            attackRadius = 0;
        } else if(colType == ColliderType.Circle) {
            attackRadius = weaponType.AttackRadius.Value;
            attackRangeX = 0;
            attackRangeY = 0;

        }
        boxAngle = weaponType.angleOfBox;
    }

    void Update() {
        if(timeBetweenAttack <= 0) {
            if(Vector2.Distance(attackPos.position, playerPos.position) < (colType == 0 ? attackRangeX : attackRadius)) {
                AttackTypeExecute(colType);
            }
            timeBetweenAttack = startTimeBtwAttack;
        } else {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void AttackTypeExecute(ColliderType colliderTypeNumber) {
        if(colliderTypeNumber == ColliderType.Square) {
            Collider2D enemyCollider = Physics2D.OverlapBox(attackPos.transform.position, new Vector2(attackRangeX, attackRangeY) * 2, boxAngle, whatIsEnemy);
            if(enemyCollider) {
                playerHealth.Variable.Value -= weaponDmg;
            }
        } else if(colliderTypeNumber == ColliderType.Circle) {
            Collider2D enemyCollider = Physics2D.OverlapCircle(attackPos.transform.position, attackRadius, whatIsEnemy);
            if(enemyCollider) {
                playerHealth.Variable.Value -= 1;
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.transform.position, attackRadius);
        Gizmos.DrawWireCube(attackPos.transform.position, new Vector3(attackRangeX, attackRangeY, 0));
    }
}
