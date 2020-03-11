using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float acceleration = 60f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] Transform cardinalBody;

    Rigidbody rb;
    Animator animator;
    GroundCollider groundCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundCollider = GetComponentInChildren<GroundCollider>();
    }

    private void OnEnable()
    {
        groundCollider.onChangeGroundState += FallingAnimation;
    }

    private void OnDisable()
    {
        groundCollider.onChangeGroundState -= FallingAnimation;
    }

    public void Move(float input)
    {
        float moveForce = input * acceleration;

        rb.AddForce(new Vector3(moveForce, 0f, 0f), ForceMode.Acceleration);

        UpdateAnimator();
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
            cardinalBody.right = new Vector3(0, 0, -1);
        }
        else if (input < 0)
        {
            cardinalBody.right = new Vector3(0, 0, 1);
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
}
