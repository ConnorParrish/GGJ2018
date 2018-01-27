using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 10f;
    public bool movementAllowed = true;

	void Update ()
    {
        // if movement is not allowed, return
        if(!movementAllowed)
        {
            return;
        }
        // move along the Horizontal and Vertical by the input managers record of those values scaled by speed
        transform.localPosition += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        transform.localPosition += new Vector3(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
    }
}
