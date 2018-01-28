using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

    // This class will only contain one method that will take care of everything we need
    // to happen when the player dies.
	public void Die()
    {
        HudManager.instance.showDeathScreen();
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].GetComponent<GuardAI>().enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.GetComponent<Movement>().movementAllowed = false;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Submit") == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
