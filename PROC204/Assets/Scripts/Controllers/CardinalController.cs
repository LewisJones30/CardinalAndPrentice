using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardinalController : MonoBehaviour
{
    const int gamepadNum = 0;

    Mover mover;
    Melee melee;
    Health health;
    Vector2 moveInput;
    bool jumpReset = true;
    bool attackReset = true;
    bool rollReset = true;

    void Awake()
    {
        mover = GetComponent<Mover>();
        melee = GetComponent<Melee>();
        health = GetComponent<Health>();
    }    

    private void Update()
    {
        if (gamepadNum >= Gamepad.all.Count) return;

        var gamepad = Gamepad.all[gamepadNum];
        if (gamepad == null) return;

        if (health.IsDead) return;

        Move(gamepad);
        Jump(gamepad);
        MeleeAttack(gamepad);
        ForwardRoll(gamepad);

        if (gamepad.buttonNorth.isPressed == true) //Y on Xbox, Triangle on PS4
        {
            //Code to be added
        }
    }

    private void ForwardRoll(Gamepad gamepad)
    {
        if (gamepad.buttonEast.isPressed && rollReset) //B on Xbox, Circle on PS4
        {
            rollReset = false;
            mover.ForwardRoll();
        }

        if (!gamepad.buttonEast.isPressed)
        {
            rollReset = true;
        }
    }

    private void MeleeAttack(Gamepad gamepad)
    {
        if (gamepad.buttonWest.isPressed && attackReset) //Player 1 melee button
        {
            attackReset = false;
            melee.Swing();
        }

        if(!gamepad.buttonWest.isPressed)
        {
            attackReset = true;
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
        if (health.IsDead) return;

        mover.Move(moveInput.x, 1f);
    }
}
