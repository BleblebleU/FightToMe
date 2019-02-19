using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExtremePathFinder))]
[RequireComponent(typeof(Rigidbody2D))]
public class FollowPath : MonoBehaviour {

    ExtremePathFinder extremePaths;
    Rigidbody2D rb2d;

    public Transform groundTransform;
    public Transform target;

    public float moveSpeedX = 5;
    public float jumpFromDistance = 10;
    public float biasX = 2;
    public float biasY = 5;

    public bool playerMoved = false;

    public List<Vector2> extremeRight;
    public List<Vector2> extremeleft;
    public List<Vector2> followPath;

    private bool followingPath = true;
    private bool pathSet = false;
    private bool rightPath = true;
    private bool jumping = false;
    private float moveDirection;

    private float[] pathsData;

    private bool reachedX = false;
    private bool reachedY = false;

    private float yAtGrounded = 999999;
    private bool yChanged = false;

    private bool selectedNewPath = false;

    private bool doOnce = false;

    private void Awake()
    {
        extremePaths = GetComponent<ExtremePathFinder>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        extremeRight = extremePaths.rightExtremePath;
        extremeleft = extremePaths.leftExtremePath;
        followPath = RandomPath(extremeleft, extremeRight);
    }
	
	// Update is called once per frame
	void Update () {

        extremeRight = extremePaths.rightExtremePath;
        extremeleft = extremePaths.leftExtremePath;

        //NEED TO MAKE IT SO THAT WILL FOLLOW PLAYER IF IT MOVES AGAIN

        bool isGrounded = Physics2D.Raycast(groundTransform.position, Vector2.down, 0.1f);
        if (!IsTargetDirectlyPlayer(followPath))
        {
            //Debug.Log("Following path which is not player");
            if ((!selectedNewPath && !jumping) || !SelectedPathValid())
            {
                followPath = RandomPath(extremeleft, extremeRight);
                selectedNewPath = true;
            }

            MoveControler();
            CheckToReset(isGrounded);
        }
        else
        {
            
            CheckToReset(isGrounded);

            StopMovement(isGrounded);
        }
    }

    private bool SelectedPathValid()
    {
        if(followPath.Count > 0)
        {
            //Debug.Log(followPath[0] == extremeleft[0] || followPath[0] == extremeRight[0]);
            return followPath[0] == extremeleft[0] || followPath[0] == extremeRight[0];
        }
        else
        {
            //Debug.Log("does not excede one");
            return false;
        }
    }

    private void MoveControler()
    {
        //Debug.Log("reachedX: " + reachedX + " [||] reachedY: " + reachedY);

        if (!reachedX && !jumping)
        {
            MoveToX(followPath, rightPath, out reachedX);
            bool isGrounded = Physics2D.Raycast(groundTransform.position, Vector2.down, 0.1f);
            if (isGrounded)
            {
                yAtGrounded = transform.position.y;
            }
        }
        if (!reachedY && reachedX)
        {
            MoveToY(followPath, reachedX, out reachedY);
        }
    }

    private void StopMovement(bool isGrounded)
    {
        if (isGrounded)
        {
            //Debug.Log("Stopping movement");
            rb2d.velocity = Vector2.zero;
        }
    }

    private void CheckToReset(bool isGrounded)
    {
        if (jumping)
        {
            if (isGrounded)
            {
                //Debug.Log("reset reached xy");
                jumping = false;
                ResetReachedXY();
                selectedNewPath = false;
            }
        }
    }

    private bool IsTargetDirectlyPlayer(List<Vector2> followPath)
    {
        if(Mathf.Floor(followPath[0].x) == Mathf.Floor(target.position.x) && Mathf.Floor(followPath[0].y) == Mathf.Floor(target.position.y))
        {
            if(Mathf.Floor(followPath[0].x) == Mathf.Floor(transform.position.x) || Mathf.Floor(followPath[0].y) == Mathf.Floor(transform.position.y))
            {
                //Debug.Log("Same");
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool ShouldMakeNewPath()
    {
        if(reachedX && reachedY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetReachedXY()
    {
        reachedX = false;
        reachedY = false;
    }

    private List<Vector2> RandomPath(List<Vector2> left, List<Vector2> right)
    {
        int randomIndex = Random.Range(0, 2);
        if (randomIndex == 0)
        {
            //Debug.Log("Set leftPath");
            rightPath = false;
            return left;
        }
        else
        {
            //Debug.Log("Set rightPath");
            rightPath = true;
            return right;
        }
    }

    private float[] RelativePosition(List<Vector2> followPath)
    {
        float[] relativeDistances = {followPath[0].x - transform.position.x, followPath[0].y - transform.position.y};
        return relativeDistances;
    }

    private void MoveToX(List<Vector2> followPath, bool rightPath, out bool reachedPointX)
    {
        reachedPointX = false;
        float relativeDistX = RelativePosition(followPath)[0];
        float relativeDistY = RelativePosition(followPath)[1];
        //Debug.Log(relativeDistX);
        //Debug.Log(reachedX);
        if (rightPath)
        {
            if (Mathf.Floor(relativeDistX) == -(jumpFromDistance + 1))
            {
                //Debug.Log("reachedX");
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
                reachedX = true;
            }
            else if (relativeDistX <= 0)
            {
                if(relativeDistX > -jumpFromDistance)
                {
                    rb2d.velocity = new Vector2(moveSpeedX * 1, rb2d.velocity.y);
                    reachedX = false;
                }
                else if(relativeDistX < -jumpFromDistance)
                {
                    rb2d.velocity = new Vector2(moveSpeedX * -1, rb2d.velocity.y);
                    reachedX = false;
                }
            }
            else if(relativeDistX > 0)
            {
                rb2d.velocity = new Vector2(moveSpeedX * 1, rb2d.velocity.y);
                reachedX = false;
            }
            
        }
        else
        {
            if (Mathf.Floor(relativeDistX) == jumpFromDistance)
            {
                //Debug.Log("reachedX");
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
                reachedX = true;
            }
            else  if (relativeDistX > 0)
            {
                if (relativeDistX > jumpFromDistance)
                {
                    rb2d.velocity = new Vector2(moveSpeedX * 1, rb2d.velocity.y);
                    reachedX = false;
                }
                else if (relativeDistX < jumpFromDistance)
                {
                    rb2d.velocity = new Vector2(moveSpeedX * -1, rb2d.velocity.y);
                    reachedX = false;
                }
            }
            else if (relativeDistX <= 0)
            {
                rb2d.velocity = new Vector2(moveSpeedX * -1, rb2d.velocity.y);
                reachedX = false;
            }
            
        }
    }

    private void MoveToY(List<Vector2> followPath, bool reachedX, out bool reachedPointY)
    {
        reachedPointY = false;
        if(rb2d.velocity.y > 0 && Mathf.Floor(transform.position.y) == Mathf.Floor(followPath[0].y))
        {
            //Debug.Log("Same Y attained");
            reachedPointY = true;
        }
        else if (rb2d.velocity.y < 0 && Mathf.Floor(transform.position.y) <= Mathf.Floor(followPath[0].y))
        {
            jumping = true;
            reachedPointY = true;
        }
        else
        {
            if (reachedX && !reachedPointY)
            {
                //Debug.Log(Mathf.Floor(transform.position.y) == Mathf.Floor(followPath[0].y));
                if (!jumping && followPath[0].y > yAtGrounded)
                {
                    float relativeDistX = RelativePosition(followPath)[0];
                    float relativeDistY = RelativePosition(followPath)[1];
                    float verticalVelocity = rb2d.velocity.y;
                    //Debug.Log("jumping and changing velocity Y"); 
                    
                    verticalVelocity = Mathf.Sqrt(2 * (-Physics2D.gravity.y) * (Mathf.Abs(relativeDistY) + biasY));
                    float halfTime = -Physics2D.gravity.y / verticalVelocity;
                    float horizontalVelocity = jumpFromDistance + biasX / 2 * halfTime;
                    //Debug.Log(verticalVelocity);
                    float dir = Mathf.Abs(relativeDistX) / relativeDistX;

                    rb2d.velocity = new Vector2(horizontalVelocity * dir, verticalVelocity);
                    jumping = true;
                    reachedX = false;
                }
            }
            else
            {
                Debug.Log("Did not reach X");
            }
        }
    }

    private float[] CalcPathFollowData()
    {
        float verticalDistance = Mathf.Abs(followPath[0].y - transform.position.y) + 2;
        float horizontalDistance = followPath[0].x - transform.position.x;
        float halfTime = horizontalDistance / 2 * moveSpeedX;
        float verticalVelocity = Mathf.Sqrt(2 * verticalDistance * 10);

        float[] pathData = { verticalDistance, horizontalDistance, halfTime, verticalVelocity };
        return pathData;
    }

    private void DirectionManager()
    {
        if(rb2d.velocity.x != 0)
        {
            Vector3 scaler = transform.localScale;

            scaler.x *= Mathf.Abs(rb2d.velocity.x) / rb2d.velocity.x;
            transform.localScale = scaler;
        }
    }
}
