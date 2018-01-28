﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

    public GameObject deathScreen;

    // This class will only contain one method that will take care of everything we need
    // to happen when the player dies.
	public void Die()
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].GetComponent<GuardAI>().enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.GetComponent<Movement>().movementAllowed = false;
        gameObject.GetComponent<PossessGuard>().canPossess = false;
        gameObject.GetComponent<DecayTracker>().decaying = false;
        gameObject.GetComponent<Animator>().SetTrigger("death");
    }

    public void ShowDeathScreen()
    {
        HudManager.instance.showDeathScreen();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Submit") == 1 && deathScreen.activeSelf)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
