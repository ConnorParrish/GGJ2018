using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessGuard : MonoBehaviour {

    public float possessionRadius = 1;
    public bool canPossess = true;

    public bool possessing = false;
    private Transform possessedGaurd;
    private GameObject candidate;
    private Animator candidate_anim;
    private Animator animController;
    private Movement playerMovement;
    private List<int> previouslyPossessed = new List<int>();
    private FollowObject mainCamFollow;

    private void Start()
    {
        animController = gameObject.GetComponent<Animator>();
        mainCamFollow = GameObject.FindWithTag("MainCamera").GetComponent<FollowObject>();
        playerMovement = GetComponent<Movement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Guard" && !previouslyPossessed.Contains(other.gameObject.GetInstanceID()))
        {
            candidate = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!possessing && candidate != null && other.tag == "Guard" && other.gameObject.GetInstanceID() == candidate.GetInstanceID())
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
            possessing = false;
            transform.SetParent(null);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            animController.SetTrigger("endingPossession");
        }

        // if we are actively possessing, follow the possessed guard
        if (possessing && transform.parent != null)
            transform.localPosition = Vector3.zero;

        // if we are hitting the possession key, look for nearby gaurds to possess and start possession on them
        if (Input.GetAxisRaw("Possess") == 1 && !possessing && candidate != null)
        {
            possessing = true;
            mainCamFollow.enabled = false;
            animController.SetTrigger("takingOver");
            //playerMovement.movingDown = candidate_anim.GetBool("movingDown");
            //playerMovement.movingUp = candidate_anim.GetBool("movingUp");
            //playerMovement.movingLeft = candidate_anim.GetBool("movingLeft");
            //playerMovement.movingRight = candidate_anim.GetBool("movingRight");
        }
    }

    public void HandlePossession()
    {
        possessing = true;
        mainCamFollow.enabled = true;
        //transform.localPosition = candidate.transform.localPosition;
        //possessedGaurd = candidate.transform;
        gameObject.GetComponent<DecayTracker>().resetDecayTime();
        //previouslyPossessed.Add(candidate.GetInstanceID());
        candidate.GetComponent<CapsuleCollider2D>().enabled = false;
        animController.SetBool("moving", false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.SetParent(candidate.transform);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        candidate_anim = transform.parent.GetComponent<Animator>();
    }

    public void AimingSneeze()
    {
        candidate_anim.enabled = false;
        possessing = false;
    }
}
