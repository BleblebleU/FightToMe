using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoves : MonoBehaviour {

    public BoolVariable playerDash;
    public BoolVariable playerDoubleJump;
    public BoolVariable playerWallClimb;

    public FloatReference extraJumps;

    public GameObject player;

    private Dash dashAbility;
    private bool secondJump;
    private bool isGrounded;
    //private bool canDoubleJump = false;

    private void Awake()
    {
        dashAbility = player.GetComponent<Dash>();
        secondJump = player.GetComponent<PlayerController>().canSecondJump;
        isGrounded = player.GetComponent<PlayerController>().isGrounded.boolState;
        dashAbility.enabled = false;
    }

    void Start () {
        ChecktToActivate();
    }
	
	void Update () {

        isGrounded = player.GetComponent<PlayerController>().isGrounded.boolState;
        secondJump = player.GetComponent<PlayerController>().canSecondJump;
        ChecktToActivate();
    }

    protected void ChecktToActivate()
    {
        if (playerDash.boolState == true && Input.GetButton("Dash"))
        {
            dashAbility.enabled = true;
        }
        else
        {
            dashAbility.enabled = false;
        }
        if (playerDoubleJump.boolState == true)
        {
            //Debug.Log("Set extra jumps to 1");
            if(isGrounded == true)
            {
                //Debug.Log("Player is Grounded");
                player.GetComponent<PlayerController>().canSecondJump = true;
            }
        }
        else
        {
            //Debug.Log("Set extra jumps to 0");
            player.GetComponent<PlayerController>().canSecondJump = false;
        }
        if(playerWallClimb.boolState == true)
        {
            //Debug.Log("Can wall Climb");
        }
        else
        {
            //Debug.Log("Cannot wall Climb");
        }
        /*if (canDoubleJump)
          { 
              
          }*/
    }

}
