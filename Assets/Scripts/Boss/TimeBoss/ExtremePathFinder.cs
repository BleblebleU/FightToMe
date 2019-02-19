using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtremePathFinder : MonoBehaviour {

    public float skinWidth = 0.01f;
    public Transform target;
    public List<GameObject> obstacleTransforms = new List<GameObject>();
    public LayerMask whatIsObstacle;
    public GameObject edgePointPrefab;
    public bool playermoved = false;
    public Transform instantiatedEdges;
    public int halfTree = 0;
    private Collider2D collider2d;

    public List<Vector2> rightPoints = new List<Vector2>();
    public List<Vector2> leftPoints = new List<Vector2>();
    public List<Vector2> rightExtremePath = new List<Vector2>();
    public List<Vector2> leftExtremePath = new List<Vector2>();

    [SerializeField] bool rightExtremeBlocked = false;
    [SerializeField] bool leftExtremeBlocked = false;

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        obstacleTransforms.Add(gameObject);
        UpdatePath();
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePath();
    }

    private void UpdatePath()
    {
        if (playermoved)
        {
            halfTree = 0;
            rightPoints.Clear();
            leftPoints.Clear();
            foreach (Transform instObj in instantiatedEdges)
            {
                instObj.GetComponent<DestroyObject>().Destroy();
            }
            FindVisiblePointTo(target, gameObject.transform.position);

            rightExtremeBlocked = CheckIfExtremesValid(rightExtremePath);
            leftExtremeBlocked = CheckIfExtremesValid(leftExtremePath);
            //playermoved = false;
        }
    }

    private bool CheckIfExtremesValid(List<Vector2> extremePaths)
    {
        for(int i = 0; i < extremePaths.Count; i++)
        {
            bool extremePathBlocked = Physics2D.OverlapBox(extremePaths[i], collider2d.bounds.size/2, 0);
            //Debug.Log("checked point: " + extremePaths[i]);
            if (extremePathBlocked)
            {
                return true;
            }
        }

        return false;
    }

    private void FindVisiblePointTo(Transform target, Vector3 caster)
    {
        Vector2 direction = (target.position - caster).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)caster + CastingPosition(direction), direction, whatIsObstacle);
        //Debug.DrawRay((Vector2)caster + CastingPosition(direction), direction, Color.cyan);
        if (hitInfo)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                if (halfTree != 0)
                {
                    List<List<Vector2>> extremePaths = ExtremePaths(halfTree, rightPoints, leftPoints);
                    if (1 <= extremePaths.Count)
                    {
                        rightExtremePath = extremePaths[0];
                        leftExtremePath = extremePaths[1];
                    }
                }
                else
                {
                    rightExtremePath.Clear();
                    rightExtremePath.Add(target.position);
                    leftExtremePath.Clear();
                    leftExtremePath.Add(target.position);
                }
                //Debug.Log("Player Found");
            }
            else
            {
                halfTree++;
                if(EdgePoints(hitInfo)!= null)
                {
                    Vector3[] edgePoints = EdgePoints(hitInfo);
                    rightPoints.Add(edgePoints[0]);
                    leftPoints.Add(edgePoints[1]);
                    for (int i = 0; i < edgePoints.Length; i++)
                    {
                        GameObject instantiatedObject = Instantiate(edgePointPrefab, edgePoints[i], Quaternion.identity, instantiatedEdges);
                        instantiatedObject.SetActive(false);
                        FindVisiblePointTo(target, edgePoints[i]);
                    }
                }
            }
        }
    }

    private List<List<Vector2>> ExtremePaths(int halfTree, List<Vector2> rightPoints, List<Vector2> leftPoints)
    {
        List<Vector2> extremeRightPath = new List<Vector2>();
        List<Vector2> extremeLeftPath = new List<Vector2>();

        for (int i = 0; i < halfTree/2 || i == 0; i++)
        {
            extremeRightPath.Add(rightPoints[i]);
            extremeLeftPath.Add(leftPoints[i]);
        }


        List<List<Vector2>> extremePaths = new List<List<Vector2>>();
        extremePaths.Add(extremeRightPath);
        extremePaths.Add(extremeLeftPath);

        return extremePaths;
    }

    private List<Vector2> PathCreator(List<Vector2> edgePoints1, List<Vector2> edgePoints2, int numberOfObstacles)
    {
        List<Vector2> path = new List<Vector2>();

        return path;
    }

    private void DebugCasting(Transform target, List<GameObject> obstacles)
    {
        Vector3 dir = DirectionSetter(target.position, transform.position);
        Debug.DrawRay(transform.position, dir);
        for (int i = 0; i < obstacleTransforms.Count; i++)
        {
            dir = DirectionSetter(target.position, obstacleTransforms[i].transform.position);
            Debug.DrawRay(obstacles[i].transform.position, dir, Color.red);
        }
    }

    private Vector3 DirectionSetter(Vector3 targetPosition, Vector3 targetFrom)
    {
        Vector3 distance = Vector3.down;
        if(targetPosition != null)
        {
            distance = targetPosition - targetFrom;
        }
        return distance.normalized;
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

}
