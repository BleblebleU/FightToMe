using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BossTestNewAi : MonoBehaviour {

    public Transform player;
    public Transform instantiatedEdges;
    public GameObject edgePointPrefab;

    public float speedX = 5;
    public float speedY = 3;
    float jumpSpeedX = 0;

    public float jumpBufferX = 3;
    public float jumpBufferY = 4;

    public const float skinWidth = 0.01f;
    public LayerMask whatIsObstacle;

    public List<Vector3> edgePoints;

    private Collider2D collider2d;
    private Rigidbody2D rb2d;
    private Vector3[] obstacleEdgePoints = new Vector3[2];

    public int randomPathIndex = 0;
    Vector2 pointToFollow = Vector2.zero;

    private bool findPath = true;
    private bool isGrounded = false;
    private float timeY = 0;
    private bool jumped = false;
    private bool jumping = false;
    private bool oldGrounded = false;

    // Use this for initialization
    void Start () {
        collider2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (findPath)
        {
            edgePoints.Clear();
            CastToTarget(player);
            randomPathIndex = RandomInt(0, edgePoints.Count);
            findPath = false;
        }
        FollowPath(randomPathIndex, edgePoints[randomPathIndex]);
	}

    private void FollowPath(int randomPathIndex, Vector2 pointToFollow)
    {
        float distY = transform.position.y - pointToFollow.y + jumpBufferY;
        pointToFollow = edgePoints[randomPathIndex];

        Debug.Log(randomPathIndex);
        MoveController(randomPathIndex, pointToFollow);

    }

    private void MoveController(int pathIndex, Vector2 pointToFollow)
    {
        float jumpPositionX = 0;
        isGrounded = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z), Vector2.down, 0.05f);
        if(oldGrounded == false && isGrounded == true)
        {
            jumping = false;
            findPath = true;
            rb2d.velocity = Vector2.zero;
        }
        oldGrounded = isGrounded;

        if (pathIndex == 0)
        {
            //Debug.Log("Point to follow X: " + pointToFollowX + "\n" + "\t" + "SUM: " + (pointToFollowX + jumpBufferX));
            jumpPositionX = (pointToFollow.x + jumpBufferX) - transform.position.x;
        }
        else if (pathIndex == 1)
        {
            //Debug.Log("Point to follow X: " + pointToFollowX + "\n" + "\t" + "SUM: " + (pointToFollowX + jumpBufferX));
            jumpPositionX = (pointToFollow.x - jumpBufferX) - transform.position.x;
        }
        //Debug.Log(jumpPositionX);

        if (!jumping)
        {
            if (MoveX(jumpPositionX) && isGrounded)
            {
                Debug.Log("Jumping");
                float diffY = pointToFollow.y - transform.position.y;
                speedY = Mathf.Sqrt(2 * -Physics2D.gravity.y * (jumpBufferY + diffY));
                timeY = Mathf.Abs(speedY / Physics2D.gravity.y);
                rb2d.velocity = new Vector2(rb2d.velocity.x, speedY);
                jumped = true;
            }
        }
        if (jumped)
        {
            float dir = (pathIndex == 0) ? -1 : 1;
            float jumpX = pointToFollow.x + (dir);
            //Debug.Log(jumpX);
            jumpSpeedX = dir * (Mathf.Abs(jumpX - transform.position.x) / timeY);
            rb2d.velocity = new Vector2(jumpSpeedX, rb2d.velocity.y);
            //Debug.Log(rb2d.velocity);
            jumped = false;
            jumping = true;
        }
    }

    private bool MoveX(float distX)
    {
        int dir = 0;
        if (Mathf.Abs(distX) >= 0.125)
        {
            dir = (int)Mathf.Sign(distX);
            rb2d.velocity = new Vector2(dir * speedX, rb2d.velocity.y);
            return false;
        }
        else
        {
            //Debug.Log("Reset x vel");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            return true;
        }
    }

    private void CastToTarget(Transform player)
    {
        Vector2 thisPos = (Vector2)gameObject.transform.position;
        Vector2 direction = ((Vector2)player.position - thisPos).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast(thisPos + CastingPosition(direction), direction, whatIsObstacle);

        if (hitInfo && !hitInfo.transform.CompareTag("Player"))
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
            //Right == edgePoint1
            //Left == edgePoint2

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
