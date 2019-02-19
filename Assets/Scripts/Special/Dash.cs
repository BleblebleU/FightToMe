using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public FloatReference HorizontalInputValue;
    public BoolVariable HorizontalInput;

    //private int HorizontalDown;
    public bool dashUsed;
    private float dashTime;

    public float dashSpeed;
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
        ResetDash();
        DashExecute();
    }

    void DashExecute()
    {
        if (HorizontalInput.boolState && dashTime < Time.time)                      //if want time to move before executing dash change HorizontalInput.boolState to HorizontalDoen == 1 so that it is executed on the next frame instead of the same one
        {
            float direction = HorizontalInputValue.Value;
            dashTime = Time.time + dashCooldownTime;
            //HorizontalDown++;
            Instantiate(dashEffect, rb.position, Quaternion.Euler(90, 0, -direction * 90));
            DashPlayer(direction);
        }
    }

    void DashPlayer(float direction)
    {
        if (HorizontalInput.boolState && direction != 0)
        {
            rb.velocity = new Vector2((Mathf.Abs(direction) / direction) * dashSpeed, rb.velocity.y);
            shakeScript.shouldShake = true;
            dashUsed = true;
            //Debug.Log("Dasing");
        }
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

        dashUsed = false;
    }
}