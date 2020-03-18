using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] PhysicMaterial stationaryMaterial;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float acceleration = 80f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform characterBody;
    [SerializeField] float rollCooldown = 1.5f;
    [SerializeField] float slopeClimbBoost = 2f;
    [SerializeField] float airAcceleration = 8f;

    Rigidbody rb;
    Animator animator;
    GroundCollider groundCollider;

    float modifedMaxSpeed;
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

    // Moves character based on input and chosen max speed
    public void Move(float input, float speedFraction)
    {
        if (Mathf.Abs(input) < Mathf.Epsilon)
        {
            SetStationary(true);
        }
        else SetStationary(false);

        modifedMaxSpeed = maxSpeed * speedFraction;

        AddForce(input);
        UpdateAnimator(input);
    }

    // Adds force if max speed not reached
    private void AddForce(float input)
    {
        if ((input < 0f) && (rb.velocity.x < -modifedMaxSpeed) ||
            ((input > 0f) && (rb.velocity.x > modifedMaxSpeed))) return;

        Vector3 charDirection = characterBody.TransformDirection(characterBody.forward);
        Vector3 moveDir = groundCollider.CalculateGroundDirection(charDirection) * input;

        Debug.DrawRay(characterBody.position, moveDir);

        // Add extra force when climbing up slopes
        if (moveDir.y > 0) moveDir.y *= slopeClimbBoost;

        rb.AddForce(CalculateForce(moveDir), ForceMode.Acceleration);
    }

    // Calculates force to add based on whether the character is on the ground or not
    private Vector3 CalculateForce(Vector3 moveDir)
    {
        Vector3 moveForce;
        if (groundCollider.IsGrounded) moveForce = moveDir * acceleration;
        else moveForce = moveDir * airAcceleration;

        return moveForce;
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

    // Updates animator based on input and move speed
    private void UpdateAnimator(float input)
    {
        float speed = rb.velocity.x;
        Turn(input);

        animator.SetFloat("forwardSpeed", Mathf.Abs(speed));
    }

    // Turns character avatar based on the horizontal input
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

    // Falling animation played when character is not touching ground
    private void FallingAnimation(bool isGrounded)
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    // Jump when on ground triggering animation and adding force
    public void Jump()
    {
        if (!groundCollider.IsGrounded) return;

        animator.SetTrigger("jumpTrigger");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    // Stationary friction force applied to prevent avatar from 
    // slipping down slopes
    public void SetStationary(bool isStationary)
    {
        if (isStationary)
        {
            GetComponent<CapsuleCollider>().material = stationaryMaterial;
        }
        else
        {
            GetComponent<CapsuleCollider>().material = null;
        }

    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
