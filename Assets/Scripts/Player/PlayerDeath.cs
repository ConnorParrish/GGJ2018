using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameObject.GetComponent<Movement>().movementAllowed = false;
    }
}
