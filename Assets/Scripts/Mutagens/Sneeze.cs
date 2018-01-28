using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneeze : MonoBehaviour {
    Animator animController;
    private bool hasSneezed;

    /// TODO: Only allow sneezing at nearby NCPS

    private void Start()
    {
        animController = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxisRaw("Sneeze") == 1 && hasSneezed == false)
        {
            hasSneezed = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            animController.SetTrigger("sneeze");
        }
	}

    public void SetTransformOffsetForAnimation()
    {
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.parent.GetComponent<GuardAI>().enabled = false;
        transform.GetComponent<PossessGuard>().enabled = false;
        transform.position += new Vector3(gameObject.GetComponent<SpriteRenderer>().size.x * 3, 0);
        transform.GetComponent<Movement>().enabled = false;
    }

    public void ResetTransformOffsetForAnimation()
    {
        transform.GetComponent<Movement>().enabled = true;
        transform.parent.GetComponent<GuardAI>().enabled = true;
        transform.GetComponent<PossessGuard>().enabled = true;
        transform.position -= new Vector3(gameObject.GetComponent<SpriteRenderer>().size.x * 3, 0);
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }
}
