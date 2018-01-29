using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerDeath : MonoBehaviour {

    public GameObject deathScreen;
    public AudioClip deathSound;

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
        gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound, 1);
    }

    public void ShowDeathScreen()
    {
        HudManager.instance.showDeathScreen();
        EndRumble();
    }

    public void StartRumble()
    {
        GamePad.SetVibration(0, 10f, 10f);
    }

    public void EndRumble()
    {
        GamePad.SetVibration(0, 0f, 0f);
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Submit") == 1 && deathScreen.activeSelf)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
