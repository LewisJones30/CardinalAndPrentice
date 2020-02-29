using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardinalController : Controller
{
    GameObject cardinal;
    GamepadController gamepadController;
    Mover mover;

    Vector2 moveInput;

    private void Awake()
    {
        gameObject.name = "Cardinal Controller";

        FindPlayer();
        SetUpControls();
    }
    private void OnEnable()
    {
        gamepadController.Cardinal.Enable();
    }

    private void OnDisable()
    {
        gamepadController.Cardinal.Disable();
    }

    private void SetUpControls()
    {
        gamepadController = new GamepadController();

        gamepadController.Cardinal.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        gamepadController.Cardinal.Movement.canceled += ctx => moveInput = Vector2.zero;

        gamepadController.Cardinal.Jump.performed += ctx => Jump();
    }

    protected override void FindPlayer()
    {
        cardinal = GameObject.FindWithTag("Player 1");
        transform.parent = cardinal.transform;
        mover = cardinal.GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        mover.Move(moveInput.x);
    }

    private void Jump()
    {
        mover.Jump();
    }
}
