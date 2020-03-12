using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrenticeController : MonoBehaviour
{
    [SerializeField] int gamepadNum = 1;

    RangedWeapon rangedWeapon;
    Shield shield;
    
    void Awake()
    {
        rangedWeapon = GetComponent<RangedWeapon>();
        shield = GetComponent<Shield>();
    }

    private void Update()
    {
        if (gamepadNum >= Gamepad.all.Count) return;

        var gamepad = Gamepad.all[gamepadNum];
        if (gamepad == null) return;

        Aim(gamepad);
        Fire(gamepad);


    }

    private void Fire(Gamepad gamepad)
    {
        if (gamepad.buttonSouth.isPressed) //A button on Xbox Controller, X on PS4
        {
            rangedWeapon.FireSouth();
        }
        if (gamepad.buttonEast.isPressed) //B button on Xbox controller, Circle on PS4
        {
            rangedWeapon.FireEast();
        }

        if (gamepad.buttonNorth.isPressed) //Y button on Xbox controller, Triangle on PS4
        {
            rangedWeapon.FireNorth();
        }

        if (gamepad.buttonWest.isPressed) //X button on Xbox controller
        {
            rangedWeapon.FireWest();
        }
    }

    private void Aim(Gamepad gamepad)
    {
        Vector2 aim;

        if (gamepad.leftStick.IsPressed()) //left joystick on Xbox/PS4
        {
            aim = gamepad.leftStick.ReadValue().normalized;
        }
        else
        {
            aim = Vector2.zero;
        }

        shield.Protect(aim);
        rangedWeapon.Aim(aim);
    }
}
