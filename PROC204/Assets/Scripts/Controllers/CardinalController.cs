using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardinalController : MonoBehaviour
{
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
        if (health.IsDead) return;

        if (Gamepad.all.Count < 1) return;

        var gamepad = Gamepad.all[0];
        if (gamepad == null) return;

        Move(gamepad);
        Jump(gamepad);
        MeleeAttack(gamepad);
        ForwardRoll(gamepad);

        mover.Move(moveInput.x, 1f);
    }

    private void ForwardRoll(Gamepad gamepad)
    {
        if (gamepad.leftShoulder.IsPressed() && rollReset) //B on Xbox, Circle on PS4
        {
            rollReset = false;
            mover.ForwardRoll();
        }

        if (!gamepad.leftShoulder.IsPressed())
        {
            rollReset = true;
        }
    }

    private void MeleeAttack(Gamepad gamepad)
    {
        if ((gamepad.leftTrigger.IsPressed() || gamepad.rightTrigger.IsPressed()) && attackReset) //Player 1 melee button
        {
            attackReset = false;
            melee.Swing();
        }

        if(!gamepad.leftTrigger.IsPressed() && !!gamepad.rightTrigger.IsPressed())
        {
            attackReset = true;
        }
    }


    private void Jump(Gamepad gamepad)
    {
        if (gamepad.rightShoulder.IsPressed() && jumpReset) //A on Xbox, X on PS4
        {
            jumpReset = false;
            mover.Jump();
        }

        if (!gamepad.rightShoulder.IsPressed())
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
}
