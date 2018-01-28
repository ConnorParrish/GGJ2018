using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMovement : MonoBehaviour {
    public float speed;
    private bool usingController;
    private Camera mainCamera;
    private Vector3 nextPos;
    public int resetCooldown;
    public int maxRadius;


    // Use this for initialization
    void Start () {
        usingController = Input.GetJoystickNames()[0] != "";

        Debug.Log((usingController ? "Using: " + Input.GetJoystickNames()[0] : "Using: Mouse"));

        mainCamera = Camera.main;
        Cursor.visible = false;

        // This is dirty, but it centers the cursor.
        Cursor.lockState = CursorLockMode.Locked;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (resetCooldown < 1)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, .1f);
            resetCooldown = 0;

            // TODO: Hide when it returns;
            if (transform.localPosition == Vector3.zero)
            {
                
            }


        }
        
        if (!usingController)
        {
//            resetCooldown = (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) ? resetCooldown - 1 : 50;

            if (Input.GetButton("Fire3"))
            {
                Vector3 nextPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                nextPos.z = 0;
                transform.position = Vector3.Lerp(transform.position, nextPos, .1f);
            }
            if (Input.GetButtonDown("Fire3"))
                Cursor.lockState = CursorLockMode.None;
            if (Input.GetButtonUp("Fire3"))
            {
                resetCooldown = 0;
                Cursor.lockState = CursorLockMode.Locked;
            }
//            transform.position = nextPos;
        }
        else
        {
            resetCooldown = (Input.GetAxis("RightJoystick X") == 0 && Input.GetAxis("RightJoystick Y") == 0) ? resetCooldown - 1 : 50;

            nextPos.x = Input.GetAxis("RightJoystick X") * speed * Time.deltaTime;
            nextPos.y = Input.GetAxis("RightJoystick Y") * speed * Time.deltaTime;
            nextPos = nextPos + transform.localPosition;
            
            if (Mathf.Abs(nextPos.x) > 15)
                nextPos.x = transform.localPosition.x;
            if (Mathf.Abs(nextPos.y) > 8)
                nextPos.y = transform.localPosition.y;
            transform.localPosition = nextPos;
        }


    }
}
