using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTestNewAi : MonoBehaviour {

    public Transform player;
    public Transform instantiatedEdges;
    public GameObject edgePointPrefab;

    public float jumpBufferX = 3;
    public float jumpBufferY = 4;

    public const float skinWidth = 0.01f;
    public LayerMask whatIsObstacle;

    public List<Vector3> edgePoints;

    private Collider2D collider2d;
    private Rigidbody2D rb2d;
    private Vector3[] obstacleEdgePoints = new Vector3[2];

    private bool onlyOnce = true;
    int randomPathIndex = 0;
    Vector2 pointToFollow = Vector2.zero;

    // Use this for initialization
    void Start () {
        collider2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        CastToTarget(player);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (onlyOnce)
        {
            randomPathIndex = RandomInt(0, edgePoints.Count);
            onlyOnce = false;
        }
        Debug.Log(edgePoints[randomPathIndex]);
        FollowPath(randomPathIndex, edgePoints[randomPathIndex]);
	}

    private void FollowPath(int randomPathIndex, Vector2 pointToFollow)
    {
        float distX = transform.position.x - pointToFollow.x;
        float distY = transform.position.y - pointToFollow.y + jumpBufferY;
        MoveX(distX);
    }

    private int MoveX(float distX)
    {
        int dir = 0;
        if (Mathf.Abs(distX) >= 0.125)
        {
            dir = (int)(-Mathf.Abs(distX) / distX);
            rb2d.velocity = new Vector2(dir * 5, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        return dir;
    }

    private void CastToTarget(Transform player)
    {
        Vector2 thisPos = (Vector2)gameObject.transform.position;
        Vector2 direction = ((Vector2)player.position - thisPos).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast(thisPos + CastingPosition(direction), direction, whatIsObstacle);

        if (!hitInfo.transform.CompareTag("Player"))
        {
            obstacleEdgePoints = EdgePoints(hitInfo);
            for (int i = 0; i < obstacleEdgePoints.Length; i++)
            {
                edgePoints.Add(obstacleEdgePoints[i]);
                GameObject instantiatedObject = Instantiate(edgePointPrefab, obstacleEdgePoints[i], Quaternion.identity, instantiatedEdges);
                instantiatedObject.SetActive(true);
            }
        }
        else
        {
            edgePoints.Add((player.position));
        }
    }

    private Vector2 CastingPosition(Vector2 direction)
    {
        float radiusX = collider2d.bounds.size.x;
        float radiusY = collider2d.bounds.size.y;

        if (direction.x > 0)
        {
            if (direction.y > 0)
            {
                return new Vector2(collider2d.bounds.extents.x + skinWidth, collider2d.bounds.extents.y + skinWidth);
            }
            else
            {
                return new Vector2(collider2d.bounds.extents.x + skinWidth, -collider2d.bounds.extents.y - skinWidth);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return new Vector2(-collider2d.bounds.extents.x - skinWidth, collider2d.bounds.extents.y + skinWidth);
            }
            else
            {
                return new Vector2(-collider2d.bounds.extents.x - skinWidth, -collider2d.bounds.extents.y - skinWidth);
            }
        }
    }

    private Vector3[] EdgePoints(RaycastHit2D hitInfo)
    {
        if (!hitInfo.transform.CompareTag("TimeBoss"))
        {
            if (hitInfo.collider.bounds.extents.x > hitInfo.collider.bounds.extents.y)
            {
                Vector3 edgePoint1 = new Vector2(hitInfo.collider.bounds.center.x + hitInfo.collider.bounds.extents.x + collider2d.bounds.extents.x, hitInfo.collider.bounds.center.y);
                Vector3 edgePoint2 = new Vector2(hitInfo.collider.bounds.center.x - hitInfo.collider.bounds.extents.x - collider2d.bounds.extents.x, hitInfo.collider.bounds.center.y);

                Vector3[] returnTransforms = { edgePoint1, edgePoint2 };
                return returnTransforms;
            }
            else
            {
                Vector3 edgePoint1 = new Vector2(hitInfo.collider.bounds.center.x, hitInfo.collider.bounds.center.y + hitInfo.collider.bounds.extents.y + collider2d.bounds.extents.y);
                Vector3 edgePoint2 = new Vector2(hitInfo.collider.bounds.center.x, hitInfo.collider.bounds.center.y - hitInfo.collider.bounds.extents.y - collider2d.bounds.extents.y);
                Vector3[] returnTransforms = { edgePoint1, edgePoint2 };
                return returnTransforms;
            }
        }
        else
        {
            Vector3[] returnTransforms = null;
            return returnTransforms;
        }
    }
    
    private int RandomInt(int inclusiveMin, int exclusiveMax)
    {
        return Random.Range(inclusiveMin, exclusiveMax);
    }
}
