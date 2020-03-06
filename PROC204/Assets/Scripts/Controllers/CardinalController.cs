using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardinalController : Controller
{
    Mover mover;
    Vector2 moveInput;
    public float movementSpeed = 0.1f;
    Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "Cardinal Controller";
        rb = this.GetComponent<Rigidbody>();
    }

    public void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!CanControl()) return;

        mover.Jump();
    }

    protected override void FindPlayer()
    {
        character = GameObject.FindWithTag("Player 1");
        if (character == null) return;

        transform.parent = character.transform;
        mover = character.GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        if (!CanControl()) return;

        mover.Move(moveInput.x);
    }
    private void Update()
    {
        if (Gamepad.all[0].leftStick.IsPressed() == true) //Only moves if player 1 is using the left stick
        {
            //transform.Translate((Input.GetAxis("Horizontal") * movementSpeed), 0, 0); //transforms the object
            rb.AddForce(Vector3.right * movementSpeed * Input.GetAxis("Horizontal"));
        }
        else if (Gamepad.all[0].buttonWest.isPressed == true) //Player 1 melee button
        {
            //Melee script here.
        }
        else if (Gamepad.all[0].buttonSouth.isPressed == true) //A on Xbox, X on PS4
        {
            //Code to be added
        }
        else if (Gamepad.all[0].buttonNorth.isPressed == true) //Y on Xbox, Triangle on PS4
        {
            //Code to be added
        }
        if (Gamepad.all[0].buttonEast.isPressed == true) //B on Xbox, Circle on PS4
        {
            //Code to be added
        }
    }
}
