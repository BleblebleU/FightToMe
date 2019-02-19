using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {
    
    [SerializeField] Collider2D deadzoneBounds;
    [SerializeField] FloatReference playerX;
    [SerializeField] FloatReference playerY;
    [SerializeField] float moveSpeedMultiplier;
    [SerializeField] FloatReference playerFallingSpeed;
    [SerializeField] BoolVariable playerIsGrounded;

    public BoolVariable followPlayerLeft;
    public BoolVariable followPlayerRight;
    public BoolVariable followPlayerDown;
    public BoolVariable followPlayerUp;

    private float oldPlayerY;

    private void FixedUpdate()
    {
        FollowTarget(playerX, playerY, followPlayerLeft, followPlayerRight, followPlayerDown, followPlayerUp);
    }

    private void FollowTarget(FloatReference targetX, FloatReference targetY, BoolVariable followLeft, BoolVariable followRight, BoolVariable followDown, BoolVariable followUp)
    {
        if((followLeft.boolState == true && targetX.Value <  transform.position.x - deadzoneBounds.bounds.extents.x) || (followRight.boolState == true && targetX.Value > transform.position.x + deadzoneBounds.bounds.extents.x))
        {
            Vector3 moveVelocity = new Vector3(targetX.Value - transform.position.x, 0, 0);
            transform.Translate(moveVelocity * moveSpeedMultiplier * Time.fixedDeltaTime);
        }

        if (followUp.boolState == true && targetY.Value > transform.position.y + deadzoneBounds.bounds.extents.y)
        {
            Vector3 moveVelocity = new Vector3(0, targetY.Value - transform.position.y, 0);
            transform.Translate(moveVelocity * moveSpeedMultiplier * Time.fixedDeltaTime);
        }

        if (followDown.boolState == true && targetY.Value < transform.position.y)
        {
            if(playerFallingSpeed.Value == 0)
            {
                //Vector3 moveVelocity = new Vector3(0, targetY.Value - transform.position.y, 0);
                //transform.Translate(moveVelocity * moveSpeedMultiplier * Time.fixedDeltaTime);
                transform.position = new Vector3(transform.position.x, targetY.Value, transform.position.z);
            }
            else
            {
                Vector3 moveVelocity = new Vector3(0, playerFallingSpeed.Value, 0);
                transform.Translate(moveVelocity * Time.fixedDeltaTime);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(deadzoneBounds.bounds.center, deadzoneBounds.bounds.size);
    }
}
