using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneeze : MonoBehaviour {
    Animator animController;
    private bool hasSneezed;

    private void Start()
    {
        animController = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxisRaw("Sneeze") == 1 && hasSneezed == false)
        {
            hasSneezed = true;
            animController.SetTrigger("sneeze");
        }
	}
}
