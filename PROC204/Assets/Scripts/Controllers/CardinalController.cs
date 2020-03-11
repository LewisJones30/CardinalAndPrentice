using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardinalController : MonoBehaviour
{
    [SerializeField] int gamepadNum = 0;

    Mover mover;
    Vector2 moveInput;
    bool jumpReset = true;

    void Awake()
    {
        mover = GetComponent<Mover>();
    }    

    private void Update()
    {
        if (gamepadNum >= Gamepad.all.Count) return;

        var gamepad = Gamepad.all[gamepadNum];
        if (gamepad == null) return;

        Move(gamepad);
        Jump(gamepad);

        if (gamepad.buttonWest.isPressed == true) //Player 1 melee button
        {
            //Melee script here.
        }

        if (gamepad.buttonNorth.isPressed == true) //Y on Xbox, Triangle on PS4
        {
            //Code to be added
        }

        if (gamepad.buttonEast.isPressed == true) //B on Xbox, Circle on PS4
        {
            //Code to be added
        }
    }

    private void Jump(Gamepad gamepad)
    {
        if (gamepad.buttonSouth.isPressed && jumpReset) //A on Xbox, X on PS4
        {
            jumpReset = false;
            mover.Jump();
        }

        if (!gamepad.buttonSouth.isPressed)
        {
            jumpReset = true;
        }
    }

    private void Move(Gamepad gamepad)
    {
        if (gamepad.leftStick.IsPressed()) //Only moves if player 1 is using the left stick
        {
            moveInput = gamepad.leftStick.ReadValue();
        }
        else
        {
            moveInput = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        mover.Move(moveInput.x);
    }
}
