using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float acceleration = 500f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform characterBody;
    [SerializeField] float rollCooldown = 1.5f;
    [SerializeField] float slopeClimbBoost = 2f;

    Rigidbody rb;
    Animator animator;
    GroundCollider groundCollider;

    bool canRoll = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundCollider = GetComponentInChildren<GroundCollider>();
    }

    private void Update()
    {
        FallingAnimation(groundCollider.IsGrounded);
    }

    public void Move(float input)
    {
        Vector3 charDirection = characterBody.TransformDirection(characterBody.forward);
        Vector3 moveDir = groundCollider.CalculateGroundDirection(charDirection) * input;

        Debug.DrawRay(characterBody.position, moveDir);

        moveDir.y *= slopeClimbBoost;
        Vector3 moveForce = moveDir * acceleration;

        rb.AddForce(moveForce, ForceMode.Acceleration);

        UpdateAnimator();
    }


    public void ForwardRoll()
    {
        if (!canRoll) return;

        canRoll = false;
        animator.SetTrigger("rollTrigger");
        Invoke(nameof(EnableRoll), rollCooldown);
    }

    private void EnableRoll()
    {
        canRoll = true;
    }

    private void UpdateAnimator()
    {
        float speed = rb.velocity.x;
        Turn(speed);

        animator.SetFloat("forwardSpeed", Mathf.Abs(speed));
    }

    private void Turn(float input)
    {
        if (Mathf.Abs(input) < 0.1f) return;

        if (input > 0)
        {
            characterBody.forward = new Vector3(1, 0, 0);
        }
        else if (input < 0)
        {
            characterBody.forward = new Vector3(-1, 0, 0);
        }
    }

    private void FallingAnimation(bool isGrounded)
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    public void Jump()
    {
        if (!groundCollider.IsGrounded) return;

        animator.SetTrigger("jumpTrigger");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
