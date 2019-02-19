using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour {

    public FloatReference currentColliderXLeft;
    public FloatReference currentColliderYDown;
    public FloatReference currentColliderXRight;
    public FloatReference currentColliderYMax;

    public Transform cameraBoundaryXLeft;
    public Transform cameraBoundaryYDown;
    public Transform cameraBoundaryXRight;
    public Transform cameraBoundaryYTop;

    public BoolVariable cameraShouldFollowLeft;
    public BoolVariable cameraShouldFollowRight;
    public BoolVariable cameraShouldFollowDown;
    public BoolVariable cameraShouldFollowUp;

    private Camera cameraComponent;
    private Vector4 cameraMinMax = new Vector4(0, 0, 0 ,0);

	// Use this for initialization
	void Start () {
        cameraComponent = GetComponent<Camera>();
        //Debug.Log((Vector2)camera.transform.position - cameraComponent.rect.size);
        //Debug.Log(cameraComponent.scaledPixelWidth);
        //Debug.Log(cameraComponent.ViewportToWorldPoint(new Vector3(cameraComponent.rect.xMin, cameraComponent.rect.yMin, cameraComponent.nearClipPlane)));
    }
	
	// Update is called once per frame
	void Update () {
        CanFollowPlayerChecker();
        cameraMinMax = CameraBoundsSetter();
        BoundaryTransformRepresentator();
        //Debug.DrawLine(cameraBoundaryXMin.transform.position, cameraBoundaryYMin.transform.position);
        //Debug.DrawLine(cameraBoundaryXMax.transform.position, cameraBoundaryYMin.transform.position);
        //Debug.DrawLine(cameraBoundaryXMin.transform.position, cameraBoundaryYMax.transform.position);
        //Debug.DrawLine(cameraBoundaryXMax.transform.position, cameraBoundaryYMax.transform.position);
        //Debug.Log(cameraMinMax.x + ", " + cameraMinMax.y + ", " + cameraMinMax.z + ", " + cameraMinMax.w);
    }

    private Vector4 CameraBoundsSetter()
    {
        Vector4 cameraMinMax = new Vector4(0, 0, 0, 0);

        Vector2 xyMin = cameraComponent.ViewportToWorldPoint(new Vector3(cameraComponent.rect.xMin, cameraComponent.rect.yMin, 0));
        //Debug.Log(xyMin.x + "," + xyMin.y);
        cameraMinMax.x = xyMin.x;
        cameraMinMax.y = xyMin.y;
        //Debug.Log(cameraMinMax.x + ", " + cameraMinMax.y);

        Vector2 xyMax = cameraComponent.ViewportToWorldPoint(new Vector3(cameraComponent.rect.xMax, cameraComponent.rect.yMax, 0));
        //Debug.Log(xyMax.x + "," + xyMax.y);
        cameraMinMax.z = xyMax.x;
        cameraMinMax.w = xyMax.y;
        //Debug.Log(cameraMinMax.z + ", " + cameraMinMax.w);
        return cameraMinMax;
    }

    private void CanFollowPlayerChecker()
    {
        if(currentColliderXLeft.Variable.Value <= cameraBoundaryXLeft.position.x)
        {
            cameraShouldFollowLeft.boolState = true;
        }
        else
        {
            cameraShouldFollowLeft.boolState = false;
        }
        if (currentColliderXRight.Variable.Value >= cameraBoundaryXRight.position.x)
        {
            cameraShouldFollowRight.boolState = true;
        }
        else
        {
            cameraShouldFollowRight.boolState = false;
        }
        if (currentColliderYDown.Variable.Value <= cameraBoundaryYDown.position.y)
        {
            cameraShouldFollowDown.boolState = true;
        }
        else
        {
            cameraShouldFollowDown.boolState = false;
        }
        if (currentColliderYMax.Variable.Value >= cameraBoundaryYTop.position.y)
        {
            cameraShouldFollowUp.boolState = true;
        }
        else
        {
            cameraShouldFollowUp.boolState = false;
        }
    }

    private void BoundaryTransformRepresentator()
    {
        cameraBoundaryXLeft.transform.position = new Vector3(cameraMinMax.x, (cameraMinMax.y + cameraMinMax.w) / 2, 0);
        cameraBoundaryYDown.transform.position = new Vector3((cameraMinMax.x + cameraMinMax.z) / 2, cameraMinMax.y, 0);
        cameraBoundaryXRight.transform.position = new Vector3(cameraMinMax.z, (cameraMinMax.y + cameraMinMax.w) / 2, 0);
        cameraBoundaryYTop.transform.position = new Vector3((cameraMinMax.x + cameraMinMax.z) / 2, cameraMinMax.w, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(cameraBoundaryXLeft.transform.position, cameraBoundaryYDown.transform.position);
        Gizmos.DrawLine(cameraBoundaryXRight.transform.position, cameraBoundaryYDown.transform.position);
        Gizmos.DrawLine(cameraBoundaryXLeft.transform.position, cameraBoundaryYTop.transform.position);
        Gizmos.DrawLine(cameraBoundaryXRight.transform.position, cameraBoundaryYTop.transform.position);
    }

}
