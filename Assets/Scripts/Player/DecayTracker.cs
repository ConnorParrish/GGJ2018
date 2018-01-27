using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayTracker : MonoBehaviour {

    public float decayTime = 10f;
    public bool decaying = true;

    float currentDecayTime;

    // start us off at full decay time
	void Start ()
    {
        currentDecayTime = decayTime;
	}
	

	void Update ()
    {
        // if we are decaying, subtract our decay time
		if (decaying)
        {
            currentDecayTime -= Time.deltaTime;
        }
        // if we have ran out of decay time, call Die on the player and stop decaying
        if (currentDecayTime < 0)
        {
            gameObject.GetComponent<PlayerDeath>().Die();
            decaying = false;
            currentDecayTime = 0;
        }
        // Update the HUD to show our proper decay time
        HudManager.instance.UpdateDeathSlider(currentDecayTime / decayTime);
	}

    public void resetDecayTime()
    {
        currentDecayTime = decayTime;
    }
}
