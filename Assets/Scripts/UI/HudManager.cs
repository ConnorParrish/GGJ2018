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

    void Start()
    {
        instance = this;

        // create connections with private
        DeathSlider = transform.Find("DeathTimer").Find("DeathSlider").GetComponent<Slider>();
        DeathCanvas = transform.Find("DeathScreen").gameObject;
    }

    public void UpdateDeathSlider(float newValue)
    {
        if (newValue > 1 || newValue < 0)
        {
            Debug.Log("You tried to set the DeathSlider to an impossible value: " + newValue);
            return;
        }

        DeathSlider.value = newValue;
    }

    public void showDeathScreen()
    {
        DeathCanvas.SetActive(true);
    }


}
