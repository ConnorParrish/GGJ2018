using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneeze : MonoBehaviour {
    Animator animController;
    SpriteRenderer spriteRenderer;
    private bool hasSneezed;
    GameObject sneezingGuard;

    /// TODO: Only allow sneezing at nearby NCPS

    private void Start()
    {
        animController = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetAxisRaw("Sneeze") == 1 && hasSneezed == false && transform.parent != null)
        {
            hasSneezed = true;
            spriteRenderer.enabled = true;
            animController.SetTrigger("sneeze");
        }
	}

    public void SetTransformOffsetForAnimation()
    {
        RaycastHit2D hit;

        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        sneezingGuard = transform.parent.gameObject;
        sneezingGuard.GetComponent<GuardAI>().enabled = false;

        if (sneezingGuard.GetComponent<GuardAI>().movingRight)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right);
        }
        else /// TODO: The rest of these
        { 
            hit = Physics2D.Raycast(transform.position, Vector2.right);
        }

        Debug.Log(transform.forward);

        if (hit.collider != null && Vector2.Distance(transform.position, hit.point) < 1 && hit.collider.tag == "Guard")
        {
            transform.SetParent(null);
            transform.GetComponent<PossessGuard>().enabled = false;
            transform.position += new Vector3(spriteRenderer.size.x * 3, 0);
            transform.GetComponent<Movement>().enabled = false;

        }
    }

    public void ResetTransformOffsetForAnimation()
    {
        hasSneezed = false;

        transform.GetComponent<Movement>().enabled = true;
        sneezingGuard.GetComponent<GuardAI>().enabled = true;
        transform.GetComponent<PossessGuard>().enabled = true;
        //transform.position -= new Vector3(gameObject.GetComponent<SpriteRenderer>().size.x * 3, 0);
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        spriteRenderer.enabled = false;        
    }
}
