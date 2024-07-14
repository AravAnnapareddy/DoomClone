using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float momentumDamping = 5f;
    private CharacterController myCC;
    //the use of public is so we can assign in the inspector
    public Animator camAnim;
    //boolean so we can keep track of where we are moving
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f;
    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();

        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        //if we are holding down wasd then it gives us a value of -1, 0, 1
        //this where we get the input to move our character

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        else
        {
            //this when you let go of the keys adds a little hover movement
            //you do not come to complete stop yet
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);
            isWalking = false;
        }

        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }

    void MovePlayer()
    {
        //where the character actually moves
        myCC.Move(movementVector * Time.deltaTime);
    }


}
