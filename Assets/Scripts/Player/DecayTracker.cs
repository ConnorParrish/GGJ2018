using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayTracker : MonoBehaviour {

    public float decayTime = 10f;
    public bool decaying = true;

    float currentDecayTime;

	void Start ()
    {
        currentDecayTime = decayTime;
	}
	
	void Update ()
    {
		if (decaying)
        {
            currentDecayTime -= Time.deltaTime;
        }
        if (currentDecayTime < 0)
        {
            gameObject.GetComponent<PlayerDeath>().Die();
            decaying = false;
            currentDecayTime = 0;
        }
        HudManager.instance.UpdateDeathSlider(currentDecayTime / decayTime);
	}
}
