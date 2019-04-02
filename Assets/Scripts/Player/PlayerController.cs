using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [Header(header:"Down Slash Bounce")]
    public float downSlashJumpDistance;
    public float downSlashJumpTimeToFall;
    public float downSlashUpVelocity;
    [Space]
    [Header(header: "Character Related")]
    public FloatReference playerHealth;
    //public BoolVariable playerWasDamaged;
    public FloatReference speed;
    [Space]
    [Header(header: "Player MoveInput Related:")]
    public FloatReference moveInputX;
    public BoolVariable moveInputedX;
    public FloatReference moveInputY;
    public BoolVariable moveInputedY;
    public BoolVariable canMoveX;
    public BoolVariable canMoveY;
    public BoolVariable dashing;
    [Space]
    [Header(header: "Player Jump Related:")]
    public float minJumpHeight;
    public float maxJumpHeight;
    public float halfJumpTime;
    public float maxJumpSpeed;
    public float minJumpSpeed;
    public BoolVariable jumpRestricted;
    public bool canSecondJump;
    public float fallMultiplyer = 2.5f;
    public float lowMultiplyer = 2.0f;
    [Space]
    [Header(header: "Player Attack Related:")]
    public Weapon currentWeapon;
    public BoolVariable playerAttacked;
    public BoolVariable damagedEnemy;
    [Space]
    [Header(header: "Player Inventory Related:")]
    public EquippedItems playerEquippedItemsList;
    public CharacterItemsList playerInventoryList;
    public BoolVariable canOpenMemoryPannel;
    [Space]
    [Header(header: "Player Physics Related:")]
    public BoolVariable isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float checkForCeelingAtHeight;
    public LayerMask whatIsCelling;
    [Space]
    [Header(header: "Player Wall Climb:")]
    public BoolVariable playerCanClimb;
    public bool wallClimbing;
    public LayerMask whatIsWall;
    public float wallSlideSpeed;
    public float wallLeapXSpeed;
    public float wallJumpYSpeed;
    [Space]
    [Header(header: "Temporary Here:")]
    public float knockBackX;
    public float knockBackY;
    public float knockBackTime;
    private bool playerKnockedBack;
    private float knockbackCoolDown;

    [SerializeField] FloatReference playerX;
    [SerializeField] FloatReference playerY;
    [SerializeField] FloatReference playerFallingSpeed;

    protected Rigidbody2D rb2d;

    public BoolVariable facingRight;

    private BoxCollider2D playerBoxCollider2d;

    private bool inputJump = false;
    private bool oldInputJump = false;

    private void OnValidate()
    {
        if (downSlashJumpTimeToFall != 0)
        {
            downSlashUpVelocity = (2 * downSlashJumpDistance) / downSlashJumpTimeToFall;
        }
        if(halfJumpTime != 0)
        {
            maxJumpSpeed = (2 * maxJumpHeight) / halfJumpTime;
        }
        if(minJumpHeight != 0)
        {
            minJumpSpeed = (2 * minJumpHeight) / halfJumpTime;
        }

        playerHealth.Variable.Value = 10;
    }

    private void Awake ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerBoxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        ResetJump();
        facingRight.boolState = true;
    }

    private void Update()
    {
        BounceBack();
        playerX.Variable.Value = transform.position.x;
        playerY.Variable.Value = transform.position.y;
        playerFallingSpeed.Variable.Value = rb2d.velocity.y;

        //Setting jumpInput
        #region SetJumpInput
        if ((Input.GetButtonDown("Vertical") || Input.GetButton("Vertical")) && Input.GetAxisRaw("Vertical") > 0)
        {
            inputJump = true;
        }
        else if (Input.GetButtonUp("Vertical"))
        {
            inputJump = false;
        }
        #endregion
        //Controlling Movement
        #region Movement
        if (!dashing.boolState)
        {
            //Debug.Log("not dashing");
            if (canMoveX.boolState == true)
            {
                MovementX(moveInputX.Value, speed, isGrounded);
            }
            else
            {
                if (playerKnockedBack == true)
                {
                    if (Time.time >= knockbackCoolDown)
                    {
                        canMoveX.boolState = true;
                        playerKnockedBack = false;
                    }
                }
            }
            if (canMoveY.boolState == true)
            {
                MovementY(moveInputY.Value, speed, isGrounded);
                WallMech(playerCanClimb);
            }
        }
        #endregion
    }

    protected void MovementX(float movementInputX, FloatReference speedToMoveX,  BoolVariable isGrounded)
    {
        //isGrounded.boolState = Physics2D.OverlapCircle((Vector2)transform.position/* + Vector2.down * 1/2*/, checkRadius, whatIsGround);
        //Debug.Log(isGrounded.boolState);
        if(isGrounded.boolState == true)
        {
            wallClimbing = false;
        }
        rb2d.velocity = (wallClimbing == false) ? new Vector2(movementInputX * speedToMoveX.Value, rb2d.velocity.y) : new Vector2(0, rb2d.velocity.y);
        if(wallClimbing == false)
        {
            if (facingRight.boolState == false && movementInputX > 0)
            {
                Flip();
            }
            else if (facingRight.boolState == true && movementInputX < 0)
            {
                Flip();
            }
        }
    }
    
    protected void MovementY(float movementInputY, FloatReference speedToMoveY, BoolVariable isGrounded)
    {
        isGrounded.boolState = Physics2D.OverlapBox((Vector2)transform.position, new Vector2(playerBoxCollider2d.bounds.size.x * 0.99f, playerBoxCollider2d.bounds.size.y + 0.05f), 0, whatIsGround);
        if (wallClimbing == false && jumpRestricted.boolState == false)
        {
            if (inputJump == true)
            {
                if (isGrounded.boolState == true)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, maxJumpSpeed);
                }
                else if (canSecondJump == true && oldInputJump != inputJump)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, maxJumpSpeed);
                    canSecondJump = false;
                }
            }
            else if (isGrounded.boolState == false && inputJump == false)
            {
                StopJump();
            }
            if (Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * checkForCeelingAtHeight, checkRadius, whatIsCelling) && rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            }
            oldInputJump = inputJump;
        }
        FastFall();
    }

    private void StopJump()
    {
        if (rb2d.velocity.y > minJumpSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, minJumpSpeed);
        }

    }

    private void WallMech(BoolVariable canWallClimb)
    {
        if (canWallClimb.boolState)
        {
            Vector3 checkPosition = new Vector3(transform.position.x + (transform.localScale.x * 0.1f), transform.position.y, transform.position.z);
            Vector3 checkCPosition = transform.position + Vector3.up;

            if (Physics2D.OverlapBox(checkPosition, playerBoxCollider2d.bounds.size, 0, whatIsWall))
            {
                bool ceelingUp = Physics2D.OverlapCircle(checkCPosition, 0.5f, whatIsCelling);
                //Debug.Log(ceelingUp);
                if (isGrounded.boolState == false && rb2d.velocity.y < 0 && !ceelingUp)
                {
                    if (jumpRestricted.boolState == false)
                    {
                        float moveDir = 0;
                        if(moveInputX.Variable.Value > 0)
                        {
                            moveDir = 1;
                        }
                        else if(moveInputX.Variable.Value < 0)
                        {
                            moveDir = -1;
                        }
                        else
                        {
                            moveDir = 0;
                        }
                        if(moveDir != 0 || moveInputedX.boolState)
                        {
                            if (moveDir != transform.localScale.x)
                            {
                                //Debug.Log("Leapt from wall");
                                rb2d.velocity = new Vector2(2 * wallLeapXSpeed * -transform.localScale.x, 3 * wallJumpYSpeed);
                                wallClimbing = true;
                            }
                            else if(inputJump && moveDir == transform.localScale.x)
                            {
                                //Debug.Log("Frog Hop from wall");
                                rb2d.velocity = new Vector2(wallLeapXSpeed * -transform.localScale.x, wallJumpYSpeed);
                                wallClimbing = true;
                            }
                            else if(moveDir == transform.localScale.x)
                            {
                                rb2d.velocity = new Vector2(rb2d.velocity.x, wallSlideSpeed);
                                wallClimbing = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Wall Sliding");
                            rb2d.velocity = new Vector2(rb2d.velocity.x, wallSlideSpeed);
                            wallClimbing = true;
                        }
                    }
                    else
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, wallSlideSpeed);
                        wallClimbing = true;
                    }
                }
            }
            else
            {
                wallClimbing = false;
            }
        }
    }

    private void FastFall()
    {
        if(rb2d.velocity.y > 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowMultiplyer) * Time.deltaTime;
        }
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer) * Time.deltaTime;
        }
        //else if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0)
        //{
        //    rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowMultiplyer - 1) * Time.deltaTime;
        //}
    }

    private void BounceBack()
    {
        if (damagedEnemy.boolState == true && moveInputY.Value == -1 && playerAttacked.boolState == true)
        {
            //Debug.Log("In HEre in BOUNCE BACK");
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(downSlashUpVelocity));
            damagedEnemy.boolState = false;
        }
    }

    private void KnockBack(Vector2 positionOfContact, float knockBackSpeedY, float knockBackSpeedX)
    {
        if(transform.position.x > positionOfContact.x)
        {
            if(transform.position.y  > positionOfContact.y)
            {
                Vector2 directioin = new Vector2(1, 1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
            else
            {
                Vector2 directioin = new Vector2(1, -1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
        }
        else
        {
            if (transform.position.y > positionOfContact.y)
            {
                Vector2 directioin = new Vector2(-1, 1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
            else
            {
                Vector2 directioin = new Vector2(-1, -1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
        }

        playerKnockedBack = true;
        //Debug.Log(rb2d.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //playerHealth.Variable.Value -= collision.gameObject.GetComponent<EnemyProperties>().damageAmount;
            canMoveX.boolState = false;
            knockbackCoolDown = Time.time + knockBackTime;
            KnockBack(collision.collider.transform.position, knockBackY, knockBackX);
            //Debug.Log(playerHealth.Variable.Value);
        }
        else if (collision.collider.CompareTag("Chest"))
        {
            canOpenMemoryPannel.boolState = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Chest"))
        {
            canOpenMemoryPannel.boolState = false;
        }
    }

    private void Flip()
    {
        facingRight.boolState = !facingRight.boolState;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void ResetJump()
    {
        canSecondJump = false;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = new Color(1, 0, 0, 0.5f);
        //Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.up * checkForCeelingAtHeight, checkRadius);
        //Gizmos.DrawCube((Vector2)transform.position, new Vector2(playerBoxCollider2d.bounds.size.x * 0.9f, playerBoxCollider2d.bounds.size.y + 0.05f));
        Gizmos.DrawWireSphere(transform.position + Vector3.up, 0.4f);
    }
}
