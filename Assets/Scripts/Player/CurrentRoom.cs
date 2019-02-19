using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour {

    public FloatReference cameraBoundXMin;
    public FloatReference cameraBoundYMin;
    public FloatReference cameraBoundXMax;
    public FloatReference cameraBoundYMax;

    private Vector3 pointXMin;
    private Vector3 pointXMax;
    private Vector3 pointYMax;
    private Vector3 pointYMin;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Room"))
        {
            //Debug.Log(collider.gameObject);
            cameraBoundXMin.Variable.Value = collider.gameObject.transform.position.x - collider.bounds.extents.x;
            cameraBoundXMax.Variable.Value = collider.gameObject.transform.position.x + collider.bounds.extents.x;
            cameraBoundYMin.Variable.Value = collider.gameObject.transform.position.y - collider.bounds.extents.y;
            cameraBoundYMax.Variable.Value = collider.gameObject.transform.position.y + collider.bounds.extents.y;

            pointXMin = new Vector3(cameraBoundXMin.Variable.Value, (cameraBoundYMin.Variable.Value + cameraBoundYMax.Variable.Value) / 2, 0);
            pointXMax = new Vector3(cameraBoundXMax.Variable.Value, (cameraBoundYMin.Variable.Value + cameraBoundYMax.Variable.Value) / 2, 0);
            pointYMin = new Vector3((cameraBoundXMin.Variable.Value + cameraBoundXMax.Variable.Value) / 2, cameraBoundYMin.Variable.Value, 0);
            pointYMax = new Vector3((cameraBoundXMin.Variable.Value + cameraBoundXMax.Variable.Value) / 2, cameraBoundYMax.Variable.Value, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(pointXMin, pointYMin);
        Gizmos.DrawLine(pointXMin, pointYMax);
        Gizmos.DrawLine(pointXMax, pointYMin);
        Gizmos.DrawLine(pointXMax, pointYMax);
    }
}
