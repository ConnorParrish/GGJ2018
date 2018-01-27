using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessGuard : MonoBehaviour {

    public float possessionRadius = 1;
    public bool canPossess = true;

    public bool possesing = false;
    private Transform possessedGaurd;
    private GameObject candidate;

    private List<int> previouslyPossessed = new List<int>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Guard" && !previouslyPossessed.Contains(other.gameObject.GetInstanceID()))
        {
            candidate = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (candidate != null && other.tag == "Guard" && other.gameObject.GetInstanceID() == candidate.GetInstanceID())
        {
            candidate = null;
        }
    }

    void Update ()
    {
        // if for any reason we are not wanted to be able to possess, return right away
        if (!canPossess)
        {
            return;
        }

        // if the player is trying to move, stop possession
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            possesing = false;
        }

        // if we are actively possessing, follow the possessed guard
        if (possesing)
        {
            transform.localPosition = possessedGaurd.localPosition;
        }

        // if we are hitting the possession key, look for nearby gaurds to possess and start possession on them
        if (Input.GetAxis("Possess") > 0 && candidate != null)
        {
            transform.localPosition = candidate.transform.localPosition;
            possesing = true;
            possessedGaurd = candidate.transform;
            gameObject.GetComponent<DecayTracker>().resetDecayTime();
            previouslyPossessed.Add(candidate.GetInstanceID());
        }
	}
}
