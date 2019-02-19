using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyPatrol : MonoBehaviour {

    [Header("Enemy Prop")]
    [SerializeField] float normalMoveSpeed = 5;
    [SerializeField] [Range(0 , 1)] private float bobbingStrength = 0.3f;
    public Transform[] waypoints;
    public bool changeWaypoint;

    private int currentWaypointIndex;
    private Vector3 originalPosition;
    private bool facingRight = true;
    private float facingDirection = 0;

    void Start () {
        originalPosition = transform.position;
	}
	
	void FixedUpdate () {
        ChangeWayPoint();
        if(changeWaypoint == true)
        {
            ChangeWayPointDueToCollision();
            changeWaypoint = false;
        }
        MoveToPosition(waypoints, currentWaypointIndex);
	}

    private void MoveToPosition(Transform[] waypoints, int currentWayPointIndex)
    {
        facingDirection = Mathf.Sign(waypoints[currentWaypointIndex].position.x - transform.position.x);
        if (facingRight == false && facingDirection > 0)
        {
            Flip();
        }
        else if (facingRight == true && facingDirection < 0)
        {
            Flip();
        }

        Vector3 moveDirection = new Vector3(waypoints[currentWayPointIndex].position.x - transform.position.x, waypoints[currentWayPointIndex].position.y - transform.position.y, 0).normalized;
        Vector3 moveVelociy = moveDirection * normalMoveSpeed;
        Vector3 moveAmount = moveVelociy * Time.deltaTime;
        transform.Translate(moveAmount);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        changeWaypoint = true;
    }

    private void ChangeWayPoint()
    {
        //Debug.Log((transform.position - waypoints[currentWaypointIndex].position).magnitude);
        if ((transform.position - waypoints[currentWaypointIndex].position).magnitude <= 0.5/* || changeWaypoint.boolState == true*/)
        {
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

    private void ChangeWayPointDueToCollision()
    {
        //Debug.Log((transform.position - waypoints[currentWaypointIndex].position).magnitude);
        if (currentWaypointIndex + 1 < waypoints.Length)
        {
            currentWaypointIndex++;
        }
        else
        {
            currentWaypointIndex = 0;
        }
    }


    private void BobThisEnemy()
    {
        transform.position = new Vector3(transform.position.x, originalPosition.y + Mathf.Sin(Time.time) * bobbingStrength, transform.position.z); 
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
