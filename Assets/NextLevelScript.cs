using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Application.LoadLevel(nextSceneID);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            nextLevelScreen.SetActive(true);
    }
}
