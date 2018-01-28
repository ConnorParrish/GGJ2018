using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : MonoBehaviour {

    public Vector3[] Path;
    public float[] Delays;
    public float speed = .3f;

    public bool movingUp = false;
    public bool movingLeft = false;
    public bool movingRight = false;
    public bool movingDown = false;
    public bool moving = false;

    private Animator animController;
    private bool backTracking = false;
    private int targetOnPath = 1;
    private Vector3 lastPos;

    private bool waiting;
    private float waitTime;

    void Start()
    {
        lastPos = transform.localPosition;
        animController = gameObject.GetComponent<Animator>();
        if(Delays.Length < Path.Length)
        {
            Delays = new float[Path.Length];
            for (int i = 0; i < Delays.Length; i++)
            {
                Delays[i] = 0;
            }
        }
    }

    void Update()
    {
        if(waiting)
        {
            waitTime -= Time.deltaTime;
            waiting = !(waitTime <= 0);
            return;
        }
        moveGuard();
        calcMovementDirection();
    }

    void calcMovementDirection()
    {
        // if we are waiting, do this and just return
        if (waiting)
        {
            animController.SetTrigger("Wait");
            moving = false;
            animController.SetBool("moving", moving);
            return;
        }

        // determine the angle we are facing
        Vector3 facing = transform.localPosition - lastPos;
        float angle = Mathf.Atan2(facing.x, facing.y) * Mathf.Rad2Deg;

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

        // rotate us to face either up, left, right, or down
        if (movingUp)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        else if (movingLeft)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 90);
        else if (movingDown)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 180);
        else if (movingRight)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 270);
    }

    void moveGuard()
    {
        // store last pos so we can know the direction we are moving in
        lastPos = transform.localPosition;
        Vector3 direction = Path[targetOnPath] - transform.localPosition;
        // if they are within a certain closeness to their target point, snap to that point and calc the next target point
        if (direction.magnitude < speed * Time.deltaTime)
        {
            transform.localPosition = Path[targetOnPath];
            calcNextTarget();
            return;
        }
        // otherwise move towards target point at speed
        else
        {
            transform.localPosition += direction.normalized * speed * Time.deltaTime;
        }
    }

    // used to move through the elements in the Path array from start to finish, then back again
    void calcNextTarget()
    {
        if(!backTracking)
        {
            targetOnPath++;
            if (targetOnPath == Path.Length)
            {
                targetOnPath -= 2;
                backTracking = true;
            }
        }
        else
        {
            targetOnPath--;
            if(targetOnPath == -1)
            {
                targetOnPath = 1;
                backTracking = false;
            }
        }

        waitTime = Delays[targetOnPath];
        waiting = waitTime != 0;
    }
}
