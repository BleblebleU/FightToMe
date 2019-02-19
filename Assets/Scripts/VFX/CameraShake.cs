using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Vector2 power;
    public float duration;
    public Transform cameraTransform;
    public float slowDownAmount = 1;
    public bool shouldShake = false;
    public BoolVariable shouldFollow;

    private Vector3 startPosition;
    private float initialDuration;
    private Vector2 RandomPosVector;

    void Start () {
        cameraTransform = Camera.main.transform;
        startPosition = cameraTransform.localPosition;
        initialDuration = duration;
	}
	
	void Update () {
        if (shouldShake)
        {
            if(duration > 0)
            {
                RandomPosVector = Random.insideUnitCircle;
                cameraTransform.position = new Vector3(transform.position.x + RandomPosVector.x * power.x, transform.position.y + RandomPosVector.y * power.y, transform.position.z) ;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
            }
        }
	}
}
