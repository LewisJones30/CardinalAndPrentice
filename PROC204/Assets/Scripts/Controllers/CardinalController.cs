using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardinalController : Controller
{
    GamepadControls gamepadControls;
    Mover mover;

    Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "Cardinal Controller";
        SetUpControls();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        gamepadControls.Cardinal.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        gamepadControls.Cardinal.Disable();
    }

    private void SetUpControls()
    {
        gamepadControls = new GamepadControls();

        gamepadControls.Cardinal.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        gamepadControls.Cardinal.Movement.canceled += ctx => moveInput = Vector2.zero;

        gamepadControls.Cardinal.Jump.performed += ctx => Jump();
    }

    protected override void FindPlayer()
    {
        character = GameObject.FindWithTag("Player 1");
        if (character == null) return;

        transform.parent = character.transform;
        mover = character.GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        if (!CanControl()) return;

        mover.Move(moveInput.x);
    }

    private void Jump()
    {
        if (!CanControl()) return;

        mover.Jump();
    }
}
