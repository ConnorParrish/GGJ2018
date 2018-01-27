using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 10f;
    public bool movementAllowed = true;

	void Update ()
    {
        if(!movementAllowed)
        {
            return;
        }
        transform.localPosition += new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0);
        transform.localPosition += new Vector3(0, Input.GetAxis("Vertical") * speed, 0);
    }
}
