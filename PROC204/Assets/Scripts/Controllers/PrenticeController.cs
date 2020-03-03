using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrenticeController : Controller
{
    RangedWeapon rangedWeapon;
    Shield shield;

    Vector2 aimInput;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "Prentice Controller";
    }

    public void OnAim(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        rangedWeapon.Fire();

        bool fire = callbackContext.ReadValue<bool>();

        //callbackContext.phase;

    }

    protected override void FindPlayer()
    {
        character = GameObject.FindWithTag("Player 2");
        if (character == null) return;

        transform.parent = character.transform;
        rangedWeapon = character.GetComponent<RangedWeapon>();
        shield = character.GetComponent<Shield>();
    }

    private void Update()
    {
        if (!CanControl()) return;

        rangedWeapon.Aim(aimInput);
        shield.Protect(aimInput);

        var gamepads = Gamepad.all;

        Gamepad prenticeController = gamepads[0];

    }
}
