using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdAI : MonoBehaviour {

    public Vector3[] Path;
    public float speed = .3f;

    private bool backTracking = false;
    private int targetOnPath = 1;

	void Start ()
    {
        transform.localPosition = Path[0];
    }

    void Update()
    {
        moveGaurd();
    }

    void moveGaurd()
    {
        Vector3 direction = Path[targetOnPath] - transform.localPosition;
        // if they are within a certain closeness to their target point, snap to that point and calc the next target point
        if (direction.magnitude < .1)
        {
            transform.localPosition = Path[targetOnPath];
            calcNextTarget();
            return;
        }

        else
        {
            transform.localPosition += direction.normalized * speed;
        }
    }

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
