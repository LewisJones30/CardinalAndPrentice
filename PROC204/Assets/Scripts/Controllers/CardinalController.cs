using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardinalController : Controller
{
    Mover mover;
    Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "Cardinal Controller";
    }

    public void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!CanControl()) return;

        mover.Jump();
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
}
