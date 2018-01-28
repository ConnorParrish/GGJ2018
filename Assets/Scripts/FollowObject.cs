using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public List<Transform> objectsToFollow = new List<Transform>(2);
    public Vector3 zOffset;

    void Start()
    {
        transform.localPosition = (objectsToFollow[0].position + objectsToFollow[1].position) / 2 + zOffset;
    }
    
	void Update ()
    {
        Vector3 nextPos = (objectsToFollow[0].position + objectsToFollow[1].position) / 2 + zOffset;
        nextPos.x = RoundToPixel(nextPos.x, 108);
        nextPos.y = RoundToPixel(nextPos.y, 108);
        transform.position = Vector3.Lerp(transform.localPosition, nextPos, .05f);
	}

    public float RoundToPixel(float input, float pixelsPerunit)
    {
        input *= pixelsPerunit;
        input = Mathf.Round(input);
        input /= pixelsPerunit;
        return input;
    }
}
