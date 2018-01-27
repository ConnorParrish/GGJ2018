using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessGuard : MonoBehaviour {

    public float possessionRadius = 1;
    public bool canPossess = true;

    private bool possesing = false;
    private Transform possessedGaurd;
	
	void Update ()
    {
        // if for any reason we are not wanted to be able to possess, return right away
        if (!canPossess)
        {
            return;
        }

        // if the player is trying to move, stop possession
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            possesing = false;
        }

        // if we are actively possessing, follow the possessed guard
        if (possesing)
        {
            transform.localPosition = possessedGaurd.localPosition;
        }

        // if we are hitting the possession key, look for nearby gaurds to possess and start possession on them
        if (Input.GetAxis("Possess") > 0)
        {
            GameObject[] allGaurds = GameObject.FindGameObjectsWithTag("Guard");
            for (int i = 0; i < allGaurds.Length; i++)
            {
                if((allGaurds[i].transform.localPosition - transform.localPosition).magnitude < possessionRadius)
                {
                    transform.localPosition = allGaurds[i].transform.localPosition;
                    possesing = true;
                    possessedGaurd = allGaurds[i].transform;
                }
            }
        }
	}
}
