using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenticeController : Controller
{
    GameObject prentice;
    GamepadController gamepadController;
    RangedWeapon rangedWeapon;

    Vector2 aimInput;

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
    }
    private void SetUpControls()
    {
        gamepadController = new GamepadController();

        gamepadController.Prentice.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        gamepadController.Prentice.Aim.canceled += ctx => aimInput = Vector2.zero;

        gamepadController.Prentice.Fire.performed += ctx => RangeAttack();
    }

    private void Update()
    {
        rangedWeapon.Aim(aimInput);
    }

    private void RangeAttack()
    {
        rangedWeapon.Fire();
    }
}
