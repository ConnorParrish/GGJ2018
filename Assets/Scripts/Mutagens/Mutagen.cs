using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mutagen : MonoBehaviour {

    public GameObject Player;
    public enum Mutation { Sneeze, Control }
    public Mutation MutationType = Mutation.Sneeze;
    public GameObject MutagenCanvas;

    private void Update()
    {
        Debug.Log(Input.GetAxisRaw("Submit"));
        if (MutagenCanvas.activeSelf == true && Input.GetAxisRaw("Submit") == 1)
        {
            MutagenCanvas.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MutagenCanvas.SetActive(true);
        if (MutationType == Mutation.Sneeze)
        {
            Debug.Log(MutagenCanvas.transform.GetChild(1).name);
            MutagenCanvas.transform.GetChild(1).GetComponent<Text>().text = "Sneeze!";
            MutagenCanvas.transform.GetChild(3).GetComponent<Text>().text = "Press 'Y' to make your host sneeze in front of them! Instantly transmits you to the other host!";
            collision.gameObject.AddComponent<Sneeze>();
        }
        //else if (MutationType == Mutation.Control)
        //    collider.gameObject.AddComponent<>

    }
}
