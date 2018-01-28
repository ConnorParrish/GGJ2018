using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// TODO: Make this work some day (the code is bad) ~Connor
public class Sneeze : MonoBehaviour {
    Animator animController;
    SpriteRenderer spriteRenderer;
    private bool sneezing;
    GameObject sneezingGuard;
    GuardAI guard_ai;
    Animator guard_anim;
    public float chargeLevel;
    public float chargeTime;
    Movement playerMovement;

    /// TODO: Only allow sneezing at nearby NCPS

    private void Start()
    {
        animController = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        if (transform.parent != null && guard_anim == null)
            guard_anim = transform.parent.GetComponent<Animator>();
        if (transform.parent == null)
            spriteRenderer.enabled = true;
        
        if (Input.GetButtonDown("Sneeze") && guard_anim.enabled == true && sneezing == false && transform.parent != null)
        {
            sneezing = true;
            spriteRenderer.enabled = true;
            //animController.SetTrigger("sneeze");
            guard_anim.SetTrigger("sneeze");
            CalculateCharge();
        }
	}

    void CalculateCharge()
    {
        while (Input.GetButton("Sneeze") && chargeLevel < 100f)
        {
            chargeLevel += Time.deltaTime * chargeTime;
        }
        
        if (chargeLevel >= 100f)
        {
            guard_anim.enabled = true;
            animController.SetTrigger("sneeze");
            chargeLevel = 0;
            sneezing = false;

            playerMovement.movingDown = guard_anim.GetBool("movingDown");
            playerMovement.movingUp = guard_anim.GetBool("movingUp");
            playerMovement.movingLeft = guard_anim.GetBool("movingLeft");
            playerMovement.movingRight = guard_anim.GetBool("movingRight");

        }
    }

    public void SetTransformOffsetForAnimation()
    {

        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        sneezingGuard = transform.parent.gameObject;
        guard_ai = sneezingGuard.GetComponent<GuardAI>();
        guard_ai.enabled = false;

        /// TODO: The rest of these

        transform.position += new Vector3(spriteRenderer.size.x * 3, 0);
    }

    private void CheckForHost()
    {
        RaycastHit2D hit;

        if (guard_ai.movingRight)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right);
        }
        else if (guard_ai.movingLeft)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left);
        }
        else if (guard_ai.movingUp)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.up);
        }
        else
            hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null && Vector2.Distance(transform.position, hit.point) < 1 && hit.collider.tag == "Guard")
        {
            transform.SetParent(null);
            transform.GetComponent<PossessGuard>().enabled = false;
            transform.GetComponent<Movement>().enabled = false;

        }

    }

    public void ResetTransformOffsetForAnimation()
    {
        sneezing = false;

        transform.GetComponent<Movement>().enabled = true;
        sneezingGuard.GetComponent<GuardAI>().enabled = true;
        transform.GetComponent<PossessGuard>().enabled = true;
        //transform.position -= new Vector3(gameObject.GetComponent<SpriteRenderer>().size.x * 3, 0);
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        spriteRenderer.enabled = false;        
    }
}
