using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.5f;
    public float smoothing = 1.5f;

    private float xMousePos;
    private float smoothedMousePos;

    private float currentLookingPos;
    //log and remove our curosers from the screen

    private void Start()
    {
        //lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        //hides cursor
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    void GetInput()
    {
        //store input 
        xMousePos = Input.GetAxisRaw("Mouse X");
    }

    void ModifyInput()
    {
        //adds sentivity
        xMousePos *= sensitivity * smoothing;
        //main smoothing is applied here
        //lerp interplates one value to another (a,b,c)
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f / smoothing);
    }

    void MovePlayer()
    {
        //this to rotate our player character
        //stores our rotation
        currentLookingPos += smoothedMousePos;
        //this is the roation now
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}
