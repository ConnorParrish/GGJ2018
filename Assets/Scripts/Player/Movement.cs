using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 10f;
    public bool movementAllowed = true;

    public bool movingLeft = false;
    public bool movingRight = false;
    public bool movingUp = false;
    public bool movingDown = false;
    public bool moving = false;

    private Vector3 lastPos;

    private Animator animController;

    void Start()
    {
        lastPos = transform.localPosition;
        animController = gameObject.GetComponent<Animator>();
    }

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

        // determine the angle we are facing
        Vector3 facing = transform.localPosition - lastPos;
        float angle = Mathf.Atan2(facing.x, facing.y) * Mathf.Rad2Deg;

        // set movement bools based on that angle value
        moving = (lastPos != transform.localPosition);
        if (moving)
        {
            movingLeft = (-45 > angle && angle > -135);
            movingDown = (-135 > angle || 135 < angle);
            movingUp = (-45 < angle && 45 > angle);
            movingRight = (45 < angle && 135 > angle);
        }
        

        animController.SetBool("movingRight", movingRight);
        animController.SetBool("movingLeft", movingLeft);
        animController.SetBool("movingUp", movingUp);
        animController.SetBool("movingDown", movingDown);
        animController.SetBool("moving", moving);


        // record the current position as our last position
        lastPos = transform.localPosition;
    }
}
