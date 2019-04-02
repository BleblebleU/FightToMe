using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public BoolVariable dash;
    public BoolVariable dashing;

    public BoolVariable shakeCamera;

    //private int HorizontalDown;
    public bool dashUsed;
    private float dashTime;

    public float dashSpeed;
    public float dashForTime;
    public float remainingDashForTime;
    public float dashCooldownTime;
    public float slowDownDashFall = 1.0f;

    public GameObject dashEffect;
    public GameObject Camera;

    private CameraShake shakeScript;
    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shakeScript = Camera.GetComponent<CameraShake>();
    }

    void FixedUpdate()
    {
        if (remainingDashForTime <= 0)
        {
            dashTime = Time.time + dashCooldownTime;
            remainingDashForTime = dashForTime;
        }
        if (dash.boolState && dashTime < Time.time)
        {
            DashExecute();
        }
        else if(dashing.boolState)
        {
            ResetDash();
        }
    }

    void DashExecute()
    {
        if (remainingDashForTime > 0)                
        {
            Instantiate(dashEffect, rb.position, Quaternion.Euler(90, 0, -90));
            DashPlayer();
        }
    }

    void DashPlayer()
    {
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
        shakeScript.shouldShake = true;
        shakeCamera.boolState = true;
        remainingDashForTime -= Time.deltaTime;
        dashing.boolState = true;
        //Debug.Log("Dasing");
    }

    void ResetDash()
    {
        //HorizontalDown = 0;
        rb.velocity = new Vector2(0, rb.velocity.y / slowDownDashFall);
        //if (dashUsed)
        //{
        //    rb.velocity = new Vector2(0, rb.velocity.y / slowDownDashFall);
        //}
        //else
        //{
        //    rb.velocity = new Vector2(HorizontalInputValue.Value, rb.velocity.y / slowDownDashFall);
        //}
        dashing.boolState = false;
    }
}