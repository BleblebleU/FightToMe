using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjects : MonoBehaviour {

    protected Rigidbody2D rb;

    // Use this for initialization
    void Awake () {

        rb = GetComponent<Rigidbody2D>();

    }

}
