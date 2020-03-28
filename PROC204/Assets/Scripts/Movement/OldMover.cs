using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMover : MonoBehaviour
{
    [SerializeField] PhysicMaterial stationaryMaterial;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float acceleration = 500f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform characterBody;
    [SerializeField] float rollCooldown = 1.5f;
    [SerializeField] float slopeClimbBoost = 2f;
    [SerializeField] float airAcceleration = 8f;

    Rigidbody rb;
    Animator animator;
    GroundCollider groundCollider;
    Health health;

    float modifedMaxSpeed;
    bool canRoll = true;
    string layerName;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundCollider = GetComponentInChildren<GroundCollider>();
        health = GetComponent<Health>();

        layerName = LayerMask.LayerToName(gameObject.layer);
    }

    private void Update()
    {
        FallingAnimation(groundCollider.IsGrounded);
        RollInvulnerability();
    }

    private void RollInvulnerability()
    {
        bool isRolling = animator.GetCurrentAnimatorStateInfo(0).IsName("Roll");

        if (isRolling || health.IsDead) gameObject.layer = LayerMask.NameToLayer("Passable");
        else gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    public void Move(float input, float speedFraction)
    {
        modifedMaxSpeed = maxSpeed * speedFraction;

        if (Mathf.Abs(input) < Mathf.Epsilon) SetStationary(true);
        else SetStationary(false);

        Vector3 charDirection = characterBody.TransformDirection(characterBody.forward);
        Vector3 moveDir = groundCollider.CalculateGroundDirection(charDirection) * input;

        Debug.DrawRay(characterBody.position, moveDir);

        if (moveDir.y > 0) moveDir.y *= slopeClimbBoost;

        Vector3 moveForce;

        if (groundCollider.IsGrounded) moveForce = moveDir * acceleration;
        else moveForce = moveDir * airAcceleration;

        if ((input < 0f) && (rb.velocity.x < -modifedMaxSpeed) || ((input > 0f) && (rb.velocity.x > modifedMaxSpeed))) return;

        rb.AddForce(moveForce, ForceMode.Acceleration);

        UpdateAnimator(input);
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

    private void UpdateAnimator(float input)
    {
        float speed = rb.velocity.x;
        Turn(input);

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
