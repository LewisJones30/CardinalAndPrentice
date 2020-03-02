using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenticeController : Controller
{
    GameObject prentice;
    GamepadController gamepadController;
    RangedWeapon rangedWeapon;
    Shield shield;

    Vector2 aimInput;
    bool fireInput = false;

    private void Awake()
    {
        gameObject.name = "Prentice Controller";

        FindPlayer();
        SetUpControls();
    }
    private void OnEnable()
    {
        gamepadController.Prentice.Enable();
    }

    private void OnDisable()
    {
        gamepadController.Prentice.Disable();
    }

    protected override void FindPlayer()
    {
        prentice = GameObject.FindWithTag("Player 2");
        transform.parent = prentice.transform;
        rangedWeapon = prentice.GetComponent<RangedWeapon>();
        shield = prentice.GetComponent<Shield>();
    }
    private void SetUpControls()
    {
        gamepadController = new GamepadController();

        gamepadController.Prentice.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        gamepadController.Prentice.Aim.canceled += ctx => aimInput = Vector2.zero;

        gamepadController.Prentice.Fire.performed += ctx => fireInput = true;
        gamepadController.Prentice.Fire.canceled += ctx => fireInput = false;
    }

    private void Update()
    {
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
