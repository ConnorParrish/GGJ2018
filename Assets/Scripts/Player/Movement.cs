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

    public bool pixelPerfectMovement;

    private Vector3 lastPos;

    private Animator animController;

    void Start()
    {
        lastPos = transform.localPosition;
        animController = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        lastPos = transform.localPosition;
    }
    void Update ()
    {
        // if movement is not allowed, return
        if(!movementAllowed)
        {
            return;
        }
        // move along the Horizontal and Vertical by the input managers record of those values scaled by speed
        Vector3 nextPos = new Vector3(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"), 0);
        moving = !(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0);

        // For pixel perfect movement
        nextPos.x = RoundToPixel(nextPos.x, 108);
        nextPos.y = RoundToPixel(nextPos.y, 108);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + nextPos * speed * Time.deltaTime);
//        transform.position += nextPos * speed * Time.deltaTime;

        // determine the angle we are facing
        Vector3 facing = transform.position + nextPos - lastPos;
        float angle = Mathf.Atan2(facing.x, facing.y) * Mathf.Rad2Deg;

        // set movement bools based on that angle value
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

    public float RoundToPixel(float input, float pixelsPerunit)
    {
        input *= pixelsPerunit;
        input = Mathf.Round(input);
        input /= pixelsPerunit;
        return input;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetType() == typeof(UnityEngine.Tilemaps.TilemapCollider2D))
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
