using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public EquippedItems playerEquippedItems;
    public Weapon currentWeapon;
    public BoolVariable damagedEnemy;

    public FloatReference currentDmg;
    private float timeBetweenAttack;
    private float startTimeBtwAttack;
    private ColliderType colType;

    private float boxAngle;

    private float attackRangeX;
    private float attackRangeY;
    private float attackRadius;

    public Transform attackPos;

    public LayerMask whatIsEnemy;

    public FloatReference verticalInputValue;
    public FloatReference horizontalInputValue;
    public BoolVariable verticalInput;
    public BoolVariable horizontalInput;

    public Collider2D[] enemyColliders;


    void Start () {
        SetCurrentWeapon();
    }
	
	void Update () {

        SetCurrentWeapon();
        //ONLY FOR DEBUGGING REMOVE OTHER WISE        //ONLY FOR DEBUGGING REMOVE OTHER WISE        //ONLY FOR DEBUGGING REMOVE OTHER WISE
        Vector3 attackPosition = AttackPosition(); // Draws THE ATACK POSITION;
        //Debug.Log(attackPosition);
        attackPos.SetPositionAndRotation(attackPosition, Quaternion.identity);
        //ONLY FOR DEBUGGING REMOVE OTHER WISE        //ONLY FOR DEBUGGING REMOVE OTHER WISE        //ONLY FOR DEBUGGING REMOVE OTHER WISE

        if (timeBetweenAttack <= 0)
        {
            if (Input.GetButton("Attack") && currentWeapon != null)
            {
                //Debug.Log("Pressed Attack");
                //Debug.Log(attackRangeX + ", " + attackRangeY);
                AttackTypeExecute(colType);
            }
            timeBetweenAttack = startTimeBtwAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
	}

    private void AttackTypeExecute(ColliderType colliderType)
    {
        if(colliderType == ColliderType.Square)
        {
            //Debug.Log("Square Collider Reached");

            enemyColliders = Physics2D.OverlapBoxAll(AttackPosition(), new Vector2(currentWeapon.AttackRangeX.Value, currentWeapon.AttackRangeY.Value) * 2, boxAngle, whatIsEnemy);

            foreach (Collider2D col2d in enemyColliders)
            {
                if(col2d is BoxCollider2D)
                {
                    col2d.GetComponent<EnemyProperties>().DamageTaken(currentDmg.Variable.Value);
                    damagedEnemy.boolState = true;
                }
            }

        }

        else if (colliderType == ColliderType.Circle)
        {
            //Debug.Log("Circle Collider Reached");
            enemyColliders = Physics2D.OverlapCircleAll(AttackPosition(), currentWeapon.AttackRadius.Value, whatIsEnemy);

            foreach (Collider2D col2D in enemyColliders)
            {
                if(col2D is BoxCollider2D)
                {
                    col2D.GetComponent<EnemyProperties>().DamageTaken(currentDmg.Variable.Value);
                    damagedEnemy.boolState = true;
                }
            }
        }
    }

    private Vector3 AttackPosition()
    {
        Vector3 attackPosition = new Vector3(transform.position.x, transform.position.y, 0);

        bool vH = Input.GetButton("Vertical");
        bool hH = Input.GetButton("Horizontal");

        if (vH)
        {
            //Debug.Log("Attacking up");
            attackPosition.x = transform.position.x;
            attackPosition.y = transform.position.y + verticalInputValue.Variable.Value;
            attackPosition.z = 0;
        }
        if (hH)
        {
            attackPosition.x = transform.position.x + horizontalInputValue.Variable.Value;
            attackPosition.y = transform.position.y;
            attackPosition.z = 0;
        }
        if (!vH && !hH)
        {
            attackPosition.x = transform.position.x;
            attackPosition.y = transform.position.y;
            attackPosition.z = 0;
        }
        return attackPosition;
    }


    private void SetCurrentWeapon()
    {
        currentWeapon = null;
        for (int i = 0; i < playerEquippedItems.equippedItems.Count; i++)
        {
            if (playerEquippedItems.equippedItems[i].equipmentType == EquipmentType.Weapon1)
            {
                currentWeapon = (Weapon)playerEquippedItems.equippedItems[i];
                break;
            }
            else
            {
                //Debug.Log("Set current weapon to null");
                currentWeapon = null;
            }
        }

        if(currentWeapon != null)
        {
            startTimeBtwAttack = currentWeapon.timeBtwAttack.Value;
            currentDmg.Variable.Value = currentWeapon.Damage.Value;
            colType = currentWeapon.ColliderType;

            boxAngle = currentWeapon.angleOfBox;
        }
        else
        {
            startTimeBtwAttack = 0;
            currentDmg.Variable.Value = 0;
            colType = 0;

            boxAngle = 0;

        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    if(currentWeapon != null)
    //    {
    //        if (currentWeapon.ColliderType == ColliderType.Circle)
    //        {
    //            Gizmos.DrawWireSphere(AttackPosition(playerAxisInputs, playerAxisBools), currentWeapon.AttackRadius.Value);
    //        }
    //        else
    //        {
    //            Gizmos.DrawWireCube(AttackPosition(playerAxisInputs, playerAxisBools), new Vector3(currentWeapon.AttackRangeX.Value, currentWeapon.AttackRangeY.Value, 0));
    //        }
    //    }
    //    else
    //    {
    //        Gizmos.DrawSphere(AttackPosition(playerAxisInputs, playerAxisBools), 0.5f);
    //    }
    //}
}
