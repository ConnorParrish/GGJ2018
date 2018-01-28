using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour {

    public int nextSceneID;
    public GameObject nextLevelScreen;

    void Update()
    {
        if (nextLevelScreen.activeSelf && Input.GetAxisRaw("Submit") == 1)
            LoadNextScene();
    }

	public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneID);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            nextLevelScreen.SetActive(true);
            collision.gameObject.GetComponent<DecayTracker>().decaying = false;
            collision.gameObject.GetComponent<Movement>().movementAllowed = false;
            GameObject[] Gaurds = GameObject.FindGameObjectsWithTag("Guard");
            for (int i = 0; i < Gaurds.Length; i++)
            {
                Gaurds[i].GetComponent<GuardAI>().enabled = false;
            }
        }
    }
}
