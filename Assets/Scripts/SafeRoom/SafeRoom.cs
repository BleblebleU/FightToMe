using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SafeRoom : MonoBehaviour {

    private BoxCollider2D boxCollider2d;

    public BoolVariable disableMovementLimiter;
    public FloatReference PlayerX;
    public FloatReference PlayerY;

    private Vector2 playerPosition;

	// Use this for initialization
	void Start () {
        boxCollider2d = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = new Vector2(PlayerX.Variable.Value, PlayerY.Variable.Value);
        CheckIfInBounds(playerPosition);
    }

    private void CheckIfInBounds(Vector3 playerPosition)
    {
        if (boxCollider2d.bounds.Contains(playerPosition))
        {
            disableMovementLimiter.boolState = true;
        }
        else
        {
            disableMovementLimiter.boolState = false;
        }
    }
}
