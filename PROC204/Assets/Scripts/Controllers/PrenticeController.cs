using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenticeController : Controller
{
    GamepadControls gamepadControls;
    RangedWeapon rangedWeapon;
    Shield shield;

    Vector2 aimInput;
    bool fireInput = false;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "Prentice Controller";
        SetUpControls();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        gamepadControls.Prentice.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        gamepadControls.Prentice.Disable();
    }

    protected override void FindPlayer()
    {
        character = GameObject.FindWithTag("Player 2");
        if (character == null) return;

        transform.parent = character.transform;
        rangedWeapon = character.GetComponent<RangedWeapon>();
        shield = character.GetComponent<Shield>();
    }
    private void SetUpControls()
    {
        gamepadControls = new GamepadControls();

        gamepadControls.Prentice.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        gamepadControls.Prentice.Aim.canceled += ctx => aimInput = Vector2.zero;

        gamepadControls.Prentice.Fire.performed += ctx => fireInput = true;
        gamepadControls.Prentice.Fire.canceled += ctx => fireInput = false;
    }

    private void Update()
    {
        if (!CanControl()) return;

        rangedWeapon.Aim(aimInput);
        shield.Protect(aimInput);
        RangeAttack();
    }

    private void RangeAttack()
    {
        if (!fireInput) return;
        rangedWeapon.Fire();
    }
}
