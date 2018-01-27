using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    // Singleton
    public static HudManager instance;

    // Privates
    Slider DeathSlider;
    GameObject DeathCanvas;
    GameObject VictorCanvas;

    void Start()
    {
        instance = this;

        // create connections with private
        DeathSlider = transform.Find("DeathTimer").Find("DeathSlider").GetComponent<Slider>();
        DeathCanvas = transform.Find("DeathScreen").gameObject;
        VictorCanvas = transform.Find("VictoryScreen").gameObject;
    }

    // Used to change the position of the slider on the bottom of the screen that tracks our closeness to death
    // Only accepts values 0-1
    public void UpdateDeathSlider(float newValue)
    {
        if (newValue > 1 || newValue < 0)
        {
            Debug.Log("You tried to set the DeathSlider to an impossible value: " + newValue);
            return;
        }

        DeathSlider.value = newValue;
    }

    // Used to show the death screen when the player dies.
    public void showDeathScreen()
    {
        DeathCanvas.SetActive(true);
    }

    public void showVictoryScreen()
    {
        VictorCanvas.SetActive(true);
    }
}
