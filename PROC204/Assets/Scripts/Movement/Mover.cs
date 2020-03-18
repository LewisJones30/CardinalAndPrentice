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

    public Vector3 Direction { get { return characterBody.forward; } }

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
        Vector3 moveForce = transform.right * input * acceleration;

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
            characterBody.right = new Vector3(0, 0, -1);
        }
        else if (input < 0)
        {
            characterBody.right = new Vector3(0, 0, 1);
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
