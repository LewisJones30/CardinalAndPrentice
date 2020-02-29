using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenticeController : Controller
{
    GameObject prentice;
    GamepadController gamepadController;
    Shoot shoot;

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
        shoot = prentice.GetComponent<Shoot>();
    }
    private void SetUpControls()
    {
        gamepadController = new GamepadController();

        gamepadController.Prentice.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        gamepadController.Prentice.Aim.performed += ctx => aimInput = Vector2.zero;
    }

    private void Update()
    {
        shoot.Aim(aimInput);
    }
}
