﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMovement : MonoBehaviour {
    public float speed;
    private bool usingController;
    private Camera mainCamera;
    private Vector3 nextPos;
    public int resetCooldown;
    public int maxRadius;
    float lastx; 
    float lasty;


    // Use this for initialization
    void Start () {
        usingController = Input.GetJoystickNames()[0] != "";

        Debug.Log((usingController ? "Using: " + Input.GetJoystickNames()[0] : "Using: Mouse"));

        mainCamera = Camera.main;
        Cursor.visible = false;

        // This is dirty, but it centers the cursor.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
	}
	
	// Update is called once per frame
	void Update () {
        if (resetCooldown < 1)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, .1f);
            resetCooldown = 0;
            
        }
        
        if (!usingController)
        {
            resetCooldown = (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) ? resetCooldown - 1 : 50;
            lastx = Input.GetAxis("Mouse X");
            lasty = Input.GetAxis("Mouse Y");
            Vector3 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            nextPos = pos;//new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            nextPos.z = 0;
            transform.position = nextPos;
        }
        else
        {
            resetCooldown = (Input.GetAxis("RightJoystick X") == 0 && Input.GetAxis("RightJoystick Y") == 0) ? resetCooldown - 1 : 50;

            nextPos.x = Input.GetAxis("RightJoystick X") * speed * Time.deltaTime;
            nextPos.y = Input.GetAxis("RightJoystick Y") * speed * Time.deltaTime;
            nextPos = nextPos + transform.localPosition;
            Debug.Log(nextPos);

            if (Mathf.Abs(nextPos.x) > 15)
                nextPos.x = 0;
            if (Mathf.Abs(nextPos.y) > 8)
                nextPos.y = 0;
            transform.localPosition = nextPos;
        }


    }
}
