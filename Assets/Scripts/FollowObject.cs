using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public List<Transform> objectsToFollow = new List<Transform>(2);
    public Vector3 zOffset;
    
	void Update ()
    {
        transform.position = (objectsToFollow[0].position + objectsToFollow[1].position) / 2 + zOffset;
	}
}
