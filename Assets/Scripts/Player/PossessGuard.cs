using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessGuard : MonoBehaviour {

    public float possessionRadius = 1;
    public bool canPossess = true;
    public AudioClip possessionSound;

    public bool possessing = false;
    private Transform possessedGaurd;
    private GameObject candidate;
    private Animator animController;
    private Movement playerMovement;
    private FollowObject mainCamFollow;

    private float delayBeforeExit = 0f;

    private void Start()
    {
        animController = gameObject.GetComponent<Animator>();
        mainCamFollow = GameObject.FindWithTag("MainCamera").GetComponent<FollowObject>();
        playerMovement = GetComponent<Movement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Guard")
        {
            candidate = other.gameObject;
        }
    }

    void Update ()
    {
        // if for any reason we are not wanted to be able to possess, return right away
        if (!canPossess)
        {
            return;
        }

        // if we are actively possessing, follow the possessed guard
        if (possessing && transform.parent != null)
            transform.localPosition = Vector3.zero;

        // if there is a delay waiting, deal with that before allowing the player to leave or possess again
        if (delayBeforeExit > 0)
        {
            delayBeforeExit -= Time.deltaTime;
            return;
        }

        // if the player is trying to move, stop possession
        if (possessing && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            gameObject.GetComponent<Movement>().enabled = true;
            possessing = false;
            candidate = null;
            transform.SetParent(null);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            animController.SetTrigger("endingPossession");
        }

        // if we are hitting the possession key, look for nearby gaurds to possess and start possession on them
        if (Input.GetAxisRaw("Possess") == 1 && !possessing && candidate != null)
        {
            possessing = true;
            mainCamFollow.enabled = false;
            animController.SetTrigger("takingOver");
            gameObject.GetComponent<Movement>().enabled = false;
            delayBeforeExit = 1f;
            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                gameObject.GetComponent<AudioSource>().PlayOneShot(possessionSound, .5f);
        }
    }

    public void HandlePossession()
    {
        mainCamFollow.enabled = true;
        gameObject.GetComponent<DecayTracker>().resetDecayTime();
        candidate.GetComponent<CapsuleCollider2D>().enabled = false;
        animController.SetBool("moving", false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.SetParent(candidate.transform);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
