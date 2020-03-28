using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform characterBody;
    [SerializeField] float rollCooldown = 1.5f;

    Animator animator;
    CharacterController charController;
    Health health;

    bool isJumping = false;
    bool canRoll = true;
    string layerName;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        layerName = LayerMask.LayerToName(gameObject.layer);
    }

    private void Update()
    {
        FallingAnimation(charController.isGrounded);
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
        float modifedMaxSpeed = maxSpeed * speedFraction;

        Vector3 movement = input * Vector3.right * modifedMaxSpeed;
        movement.z = 0f;

        if (isJumping)
        {
            if (charController.isGrounded) movement.y += jumpForce;
            isJumping = false;
        }

        charController.SimpleMove(movement);

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
        Turn(input);

        animator.SetFloat("forwardSpeed", Mathf.Abs(charController.velocity.x));
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
        if (isJumping) return;

        isJumping = true;

        animator.SetTrigger("jumpTrigger");
        charController.SimpleMove(Vector3.up * jumpForce * 1000);
    }

    public Vector3 GetVelocity()
    {
        return charController.velocity;
    }
}
