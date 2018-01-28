using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBlob : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.isTrigger)
        {
            if(!other.GetComponent<PossessGuard>().possessing)
                other.GetComponent<Animator>().SetTrigger("death");
        }
    }
}
