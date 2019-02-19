using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour {

    public FloatReference speed;
    public Transform[] waypoints;

    private int currentWaypointIndex;
    private float direction = 0;
    private bool facingRight = true;

    void Awake()
    {
        currentWaypointIndex = 0;
    }

    void FixedUpdate()
    {
        ChangeWayPoint();
        Move(speed);
    }

    protected void Move(FloatReference speedToMove)
    {
        direction = Mathf.Sign(waypoints[currentWaypointIndex].position.x - transform.position.x);
        Vector2 velocity = new Vector2(direction * speed.Value, 0);
        Vector2 moveAmount = velocity * Time.deltaTime;
        transform.Translate(moveAmount);

        if (facingRight == false && direction > 0)
        {
            Flip();
        }
        else if (facingRight == true && direction < 0)
        {
            Flip();
        }
    }

    private void ChangeWayPoint()
    {
        if(direction < 0)
        {
            if (transform.position.x <= waypoints[currentWaypointIndex].position.x)
            {
                //Debug.Log("Changed Way Point to: " + currentWaypointIndex);
                if (currentWaypointIndex + 1 < waypoints.Length)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    currentWaypointIndex = 0;
                }
            }
        }
        else
        {
            if (transform.position.x >= waypoints[currentWaypointIndex].position.x)
            {
                //Debug.Log("Changed Way Point to: " + currentWaypointIndex);
                if (currentWaypointIndex + 1 < waypoints.Length)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    currentWaypointIndex = 0;
                }
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
