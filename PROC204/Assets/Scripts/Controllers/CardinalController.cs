using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CardinalController : Controller
{
    /* SINGLEPLAYER CONTROLS (Cardinal & Prentice)
     * 
     * CARDINAL
     * Move - leftstick
     * Jump - right shoulder
     * Attack - right trigger
     * Roll - lefstick press
     * Run - left trigger
     * Parry - left shoulder
     * 
     * PRENTICE
     * Aim - rightstick / D-pad
     * Shoot yellow - Y XBox / Triangle PS4
     * Shoot red - B XBox / Circle PS4
     * Shoot blue - X XBox / Square PS4
     * Shoot green - A XBox / X PS4
     * 
     * MULTIPLAYER CONTROLS (Cardinal only)
     * 
     * Move - leftstick
     * Jump - A XBox / X PS4 / right shoulder
     * Attack - X XBox / Square PS4 / right trigger
     * Roll - Y XBox / Triangle PS4 / leftstick press
     * Run - left trigger
     * Parry - B XBox / Circle PS4 / left shoulder
     */

    //CACHE REFERENCES
    Mover mover;
    Fighter fighter;
    Health health;
    CombatTarget combatTarget;
    
    //STATES
    bool jumpReset = true;
    bool rollReset = true;
    bool parryReset = true;
    Vector2 moveInput;

    //Singleplayer or multiplayer controls
    bool isOneController;

    void Awake()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        combatTarget = GetComponent<CombatTarget>();
    }    

    //Input taken each frame
    private void Update()
    {
        if (Gamepad.all.Count < 1) return; //Controllers plugged in?

        if (Gamepad.all.Count == 1) isOneController = true; //Singleplayer
        else isOneController = false; //Multiplayer

        var gamepad = Gamepad.all[0];
        if (gamepad == null) return; //Validate first controller

        Dead(gamepad);

        if (isFrozen) return; //Player input blocked?

        Move(gamepad);
        Jump(gamepad);
        MeleeAttack(gamepad);
        ForwardRoll(gamepad);
        Dash(gamepad);
        Parry(gamepad);

        mover.Move(moveInput, 1f);
    }

    //Allows player 1 to navigate back to main menu or restart
    private void Dead(Gamepad gamepad)
    {
        if (!health.IsDead) return;

        if (gamepad.buttonSouth.IsPressed())
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (gamepad.startButton.IsPressed())
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    private void Parry(Gamepad gamepad)
    {
        bool isParry;
        if (isOneController) isParry = gamepad.leftShoulder.IsPressed();
        else isParry = gamepad.leftShoulder.IsPressed() || gamepad.buttonEast.IsPressed();

        if (isParry && parryReset)
        {
            parryReset = false;
            combatTarget.Parry();
        }

        if (!isParry) parryReset = true;
    }

    private void Dash(Gamepad gamepad)
    {
        if (gamepad.leftTrigger.IsPressed())
        {
            mover.IsDashing = true;
        }
        else
        {
            mover.IsDashing = false;
        }
    }

    private void ForwardRoll(Gamepad gamepad)
    {
        bool isRolling;
        if (isOneController) isRolling = gamepad.leftStickButton.IsPressed();
        else isRolling = gamepad.buttonNorth.IsPressed() || gamepad.leftStickButton.IsPressed();

        if (isRolling && rollReset)
        {
            rollReset = false;
            mover.ForwardRoll();
        }

        if (!isRolling)
        {
            rollReset = true;
        }
    }

    //Player can hold down attack to continuously attack
    private void MeleeAttack(Gamepad gamepad)
    {
        bool isAttack;
        if (isOneController) isAttack = gamepad.rightTrigger.IsPressed();
        else isAttack = gamepad.buttonWest.IsPressed() || gamepad.rightTrigger.IsPressed();

        if (isAttack) fighter.Attack();
    }

    private void Jump(Gamepad gamepad)
    {
        bool isJump;

        if (isOneController) isJump = gamepad.rightShoulder.IsPressed();
        else isJump = gamepad.buttonSouth.IsPressed() || gamepad.rightShoulder.IsPressed();

        if (isJump && jumpReset)
        {
            jumpReset = false;
            mover.Jump();
        }

        if (!isJump)
        {
            jumpReset = true;
        }
    }

    private void Move(Gamepad gamepad)
    {
        if (gamepad.leftStick.IsPressed())
        {
            moveInput = gamepad.leftStick.ReadValue();
        }
        else //No movement if no input
        {
            moveInput = Vector3.zero;
        }
    }
}
