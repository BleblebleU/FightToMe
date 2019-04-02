using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestroyButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyOnClick()
    {
        Destroy(gameObject);
    }
}
