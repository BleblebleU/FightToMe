using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BehaviourManager : MonoBehaviour {

    public FollowPath followPathScript;

    public bool phaseTwo = false;
    public float slowDownFactor = 0.1f;

    [Header("Beam LineRenderer")]
    public LineRenderer beamR;
    public LineRenderer beamL;
    public LineRenderer beamU;
    public LineRenderer beamD;
    [Space]

    [Header("Beam Transforms")]
    public Transform beamRi;
    public Transform beamLe;
    public Transform beamUp;
    public Transform beamDo;
    [Space]


    [Header("Beam Collider")]
    public BoxCollider2D beamColliderX;
    public BoxCollider2D beamColliderY;

    private ContactFilter2D contactFilter2d;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {

        contactFilter2d.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2d.useLayerMask = true;
        contactFilter2d.useTriggers = false;
	}
	
	// Update is called once per frame
	void Update () {

        bool[] dirs = PlayerVisibleOnAnyAxis();
        bool playerVisible = false;

        for (int i= 0; i < dirs.Length; i++)
        {
            if(dirs[i] == true)
            {
                Debug.Log("Player is Visible");
                PlayerSpotted();
                playerVisible = true;
                dirs[i] = false;
                break;
            }
        }

        if (!playerVisible)
        {
            PlayerHasMovedOutOfBeam();
        }
        PhaseTwo();
        //transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    }

    private void PlayerSpotted()
    {
        //rb2d.bodyType = RigidbodyType2D.Static;
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        CreateBeam();
        beamColliderX.size = new Vector2(100, beamColliderX.size.y);
        beamColliderY.size = new Vector2(beamColliderY.size.x, 100);
    }

    private void PlayerHasMovedOutOfBeam()
    {
        Time.timeScale = 1f;
        //Time.fixedDeltaTime = Time.timeScale;
        TurnOffBeam();
    }

    private bool[] PlayerVisibleOnAnyAxis()
    {
        bool left = false; 
        bool right = false; 
        bool up = false; 
        bool down = false;

        RaycastHit2D[] hitRight = new RaycastHit2D[1];
        int hitRightInt = Physics2D.Raycast(transform.position + (Vector3.right * 0.51f), Vector2.right, contactFilter2d, hitRight);

        RaycastHit2D[] hitLeft = new RaycastHit2D[1];
        int hitLeftInt = Physics2D.Raycast(transform.position + (Vector3.left * 0.51f), Vector2.left, contactFilter2d, hitLeft);

        RaycastHit2D[] hitUp = new RaycastHit2D[1];
        int hitUpInt = Physics2D.Raycast(transform.position + (Vector3.up * 0.51f), Vector2.up, contactFilter2d, hitUp);

        RaycastHit2D[] hitDown = new RaycastHit2D[1];
        int hitDownInt = Physics2D.Raycast(transform.position + (Vector3.down * 0.51f), Vector2.down, contactFilter2d, hitDown);

        if (hitRightInt != 0 && hitRight[0].collider.CompareTag("Player"))
        {
            right = true;
        }

        if (hitLeftInt != 0 && hitLeft[0].collider.CompareTag("Player"))
        {
            left = true;
        }

        if (hitUpInt != 0 && hitUp[0].collider.CompareTag("Player"))
        {
            up = true;
        }


        if (hitDownInt != 0 && hitDown[0].collider.CompareTag("Player"))
        {
            down = true;
        }

        bool[] detectedPlayer = { left, right, up, down };
        return detectedPlayer;
    }

    private void CreateBeam()
    {
        RaycastHit2D[] hitRight = new RaycastHit2D[1];
        int hitRightInt = Physics2D.Raycast(transform.position + (Vector3.right * 0.51f), Vector2.right, contactFilter2d, hitRight);
        float drawDistanceR = 50f;
        if (hitRightInt != 0 && !hitRight[0].collider.CompareTag("Player"))
        {
            drawDistanceR = hitRight[0].distance;
        }
        beamR.SetPosition(0, transform.position);
        beamR.SetPosition(1, transform.position + Vector3.right * (drawDistanceR + 0.5f));
        beamR.enabled = true;

        RaycastHit2D[] hitLeft = new RaycastHit2D[1];
        int hitLeftInt = Physics2D.Raycast(transform.position + (Vector3.left * 0.51f), Vector2.left, contactFilter2d, hitLeft);
        float drawDistanceL = 50f;
        if (hitLeftInt != 0 && !hitLeft[0].collider.CompareTag("Player"))
        {
            drawDistanceL = hitLeft[0].distance;
        }
        beamL.SetPosition(0, transform.position);
        beamL.SetPosition(1, transform.position + Vector3.left * (drawDistanceL + 0.5f));
        beamL.enabled = true;

        RaycastHit2D[] hitUp = new RaycastHit2D[1];
        int hitUpInt = Physics2D.Raycast(transform.position + (Vector3.up * 0.51f), Vector2.up, contactFilter2d, hitUp);
        float drawDistanceU = 50f;
        if (hitUpInt != 0 && !hitUp[0].collider.CompareTag("Player"))
        {
            drawDistanceU = hitUp[0].distance;
        }
        beamU.SetPosition(0, transform.position);
        beamU.SetPosition(1, transform.position + Vector3.up * (drawDistanceU + 0.5f));
        beamU.enabled = true;

        RaycastHit2D[] hitDown = new RaycastHit2D[1];
        int hitDownInt = Physics2D.Raycast(transform.position + (Vector3.down * 0.51f), Vector2.down, contactFilter2d, hitDown);
        Debug.DrawRay(transform.position + (Vector3.down * 0.51f), Vector3.down, Color.cyan);
        float drawDistanceD = 50f;
        if (hitDownInt != 0 && !hitDown[0].collider.CompareTag("Player"))
        {
            //Debug.Log("collision below: " + hitDown[0].distance);
            drawDistanceD = hitDown[0].distance;
        }
        beamD.SetPosition(0, transform.position);
        beamD.SetPosition(1, transform.position + Vector3.down * (drawDistanceD + 0.5f));
        beamD.enabled = true;

        //Debug.Log("drawDistanceR: " + drawDistanceR);
        //Debug.Log("drawDistanceL: " + drawDistanceL);
        //Debug.Log("drawDistanceU: " + drawDistanceU);
        //Debug.Log("drawDistanceD: " + drawDistanceD);

    }

    private void TurnOffBeam()
    {
        beamR.enabled = false;
        beamL.enabled = false;
        beamU.enabled = false;
        beamD.enabled = false;
    }

    private void PhaseTwo()
    {
        Debug.Log("Rotating around player");
        beamRi.RotateAround(Vector3.zero, Vector3.forward, 100 * Time.deltaTime);
    }

    private void RotateAround(Transform rotateAround, Vector3 rotationAxis, float radius, float speed)
    {

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + (Vector3.right * 0.51f), Vector2.right, Color.green);
        Debug.DrawRay(transform.position + (Vector3.left * 0.51f), Vector2.left, Color.green);
        Debug.DrawRay(transform.position + (Vector3.up * 0.51f), Vector2.up, Color.green);
        Debug.DrawRay(transform.position + (Vector3.down * 0.51f), Vector2.down, Color.green);
    }
}
