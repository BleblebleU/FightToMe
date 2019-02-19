using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public BoolVariable grounded;

    private int extraJumps;
    public int totalExtraJumps;
    public float jumpSpeed;

    private float jumpTimeCounter;
    public float jumpTimer;
    private bool isJumping;

    public float fallMultiplyer = 2.5f;
    public float lowMultiplyer = 2.0f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ResetJump();
    }

    void Update()
    {
        FastFall();
        DoubleJumpExecute();
    }

    void DoubleJumpExecute()
    {

        if (grounded.boolState)
        {
            ResetJump();
            //Debug.Log("isGrounded : " + isGrounded);
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            isJumping = true;
            extraJumps--;
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                jumpTimeCounter -= Time.deltaTime;
                //Debug.Log("DoubleJumpWorking2");
            }
            else
            {
                isJumping = false;
            }

        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

    }

    void FastFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowMultiplyer - 1) * Time.deltaTime;
        }
    }

    void ResetJump()
    {
        extraJumps = totalExtraJumps;
        jumpTimeCounter = jumpTimer;
        isJumping = false;
    }
}
