using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : MonoBehaviour {

    public Vector3[] Path;
    public float speed = .3f;

    public bool movingUp = false;
    public bool movingLeft = false;
    public bool movingRight = false;
    public bool movingDown = false;

    private bool backTracking = false;
    private int targetOnPath = 1;
    private Vector3 lastPos;

    void Start()
    {
        transform.localPosition = Path[0];
        lastPos = transform.localPosition;
    }

    void Update()
    {
        moveGuard();
        calcMovementDirection();
    }

    void calcMovementDirection()
    {
        // determine the angle we are facing
        Vector3 facing = transform.localPosition - lastPos;
        float angle = Mathf.Atan2(facing.x, facing.y) * Mathf.Rad2Deg;

        // set movement bools based on that angle value
        movingLeft = (-45 > angle && angle > -135);
        movingDown = (-135 > angle || 135 < angle);
        movingUp = (-45 < angle && 45 > angle);
        movingRight = (45 < angle && 135 > angle);

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
    }
}
