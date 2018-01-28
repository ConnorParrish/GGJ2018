using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutagen : MonoBehaviour {

    public GameObject Player;
    public enum Mutation { Sneeze, Control }
    public Mutation MutationType = Mutation.Sneeze;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (MutationType == Mutation.Sneeze)
            collision.gameObject.AddComponent<Sneeze>();
        //else if (MutationType == Mutation.Control)
        //    collider.gameObject.AddComponent<>

        gameObject.SetActive(false);
    }
}
