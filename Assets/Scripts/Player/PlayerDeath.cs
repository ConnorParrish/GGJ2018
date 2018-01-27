using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    // This class will only contain one method that will take care of everything we need
    // to happen when the player dies.
	public void Die()
    {
        HudManager.instance.showDeathScreen();
    }
}
