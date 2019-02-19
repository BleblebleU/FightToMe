using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MakeThisGrounded : MonoBehaviour {

    public LayerMask whatIsGround;
    private Collider2D collider2d;
    // Use this for initialization

    private void OnValidate()
    {
        collider2d = gameObject.GetComponent<Collider2D>();
        GroundThisObject();
    }

    private void Start()
    {
        GroundThisObject();
    }

    private void GroundThisObject()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - collider2d.bounds.extents.y, transform.position.z), Vector2.down, Mathf.Infinity, whatIsGround);
        if(hitInfo == true)
        {
            float distanceToGround = hitInfo.distance;
            if(distanceToGround > collider2d.bounds.size.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - distanceToGround, transform.position.z);
            }
        }
    }
}
