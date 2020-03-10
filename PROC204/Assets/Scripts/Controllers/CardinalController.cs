using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardinalController : Controller
{
    Mover mover;

    Vector2 moveInput;
    bool jumpReset = true;

    protected override void Awake()
    {
        base.Awake();
        mover = GetComponent<Mover>();
    }

    protected override void FindPlayer()
    {
        character = GameObject.FindWithTag("Player 1");
        if (character == null) return;

        transform.parent = character.transform;
        mover = character.GetComponent<Mover>();
    }

    

    private void Update()
    {
        if (Gamepad.all[0].leftStick.IsPressed()) //Only moves if player 1 is using the left stick
        {
            moveInput = Gamepad.all[0].leftStick.ReadValue();
        }
        else
        {
            moveInput = Vector3.zero;
        }

        if (Gamepad.all[0].buttonSouth.isPressed && jumpReset) //A on Xbox, X on PS4
        {
            print("JUMP");
            jumpReset = false;
            mover.Jump();
        }
        
        if (!Gamepad.all[0].buttonSouth.isPressed)
        {
            jumpReset = true;
        }

        if (Gamepad.all[0].buttonWest.isPressed == true) //Player 1 melee button
        {
            //Melee script here.
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

    private void FixedUpdate()
    {
        mover.Move(moveInput.x);
    }
}
