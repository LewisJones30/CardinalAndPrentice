using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrenticeController : MonoBehaviour
{
    Health health;
    PrenticeAttack rangedWeapon;
    ShieldPower shield;
    
    void Awake()
    {
        health = GetComponentInParent<Health>();
        rangedWeapon = GetComponent<PrenticeAttack>();
        shield = GetComponent<ShieldPower>();
    }

    private void Update()
    {
        if (health.IsDead) return;

        if (Gamepad.all.Count < 1) return;

        Gamepad gamepad;
        
        if (Gamepad.all.Count < 2) //If only one controller connected Cardinal can control Prentice as well
        {
            gamepad = Gamepad.all[0];
        }
        else // Otherwise only player 2 can control Prentice
        {
            gamepad = Gamepad.all[1];
        }

        if (gamepad == null) return;

        Aim(gamepad);
        Fire(gamepad);
    }

    private void Fire(Gamepad gamepad)
    {
        if (gamepad.buttonSouth.isPressed) //A button on Xbox Controller, X on PS4
        {
            rangedWeapon.Fire(ColourValue.Green);
        }
        if (gamepad.buttonEast.isPressed) //B button on Xbox controller, Circle on PS4
        {
            rangedWeapon.Fire(ColourValue.Red);
        }

        if (gamepad.buttonNorth.isPressed) //Y button on Xbox controller, Triangle on PS4
        {
            rangedWeapon.Fire(ColourValue.Yellow);
        }

        if (gamepad.buttonWest.isPressed) //X button on Xbox controller
        {
            rangedWeapon.Fire(ColourValue.Blue);
        }
    }

    private void Aim(Gamepad gamepad)
    {
        Vector2 aim;

        if (gamepad.rightStick.IsPressed()) //left joystick on Xbox/PS4
        {
            aim = gamepad.rightStick.ReadValue().normalized;
        }
        else if (gamepad.dpad.IsPressed())
        {
            aim = gamepad.dpad.ReadValue().normalized;
        }
        else
        {
            aim = Vector2.zero;
        }

        shield.Protect(aim);
        rangedWeapon.Aim(aim);
    }
}
