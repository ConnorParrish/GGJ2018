using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform objectToFollow;
    public bool preserveOffset = false;

    private Vector3 offset;

	void Start () {
		if (preserveOffset)
        {
            offset = transform.localPosition - objectToFollow.localPosition;
        }
        else
        {
            offset = new Vector3(0, 0, transform.localPosition.z);
        }
	}
	
	void Update ()
    {
        transform.localPosition = objectToFollow.localPosition + offset;
	}
}
