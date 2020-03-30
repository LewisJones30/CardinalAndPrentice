using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float gravity = 40f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform characterBody;
    [SerializeField] float rollCooldown = 1.5f;
    [SerializeField] float maxFallSpeed = 100f;

    Animator animator;
    CharacterController charController;
    Health health;
    float yVelocity;

    bool isJumping = false;
    bool canRoll = true;
    string layerName;
    Vector3 movement = Vector3.zero;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        layerName = LayerMask.LayerToName(gameObject.layer);
    }

    private void Update()
    {
        CalculateYVelocity();
        print(movement.y);
        charController.Move(movement * Time.deltaTime);
        movement = Vector3.zero;

        RollInvulnerability();
        PassPlatforms();
        FallingAnimation(charController.isGrounded);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void PassPlatforms()
    {
        bool canPass;

        //Can pass through platform when travelling up and not touching the ground
        if (charController.velocity.y > 0 && !charController.isGrounded) canPass = true;
        else canPass = false;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer(layerName), LayerMask.NameToLayer("Platform"), canPass);
    }

    private void RollInvulnerability()
    {
        bool isRolling = animator.GetCurrentAnimatorStateInfo(0).IsName("Roll");

        if (isRolling || health.IsDead) gameObject.layer = LayerMask.NameToLayer("Passable");
        else gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    public void Move(float input, float speedFraction)
    {
        float moveSpeed = maxSpeed * speedFraction;

        movement = input * Vector3.right * moveSpeed;

        UpdateAnimator(input);
    }

    private void CalculateYVelocity()
    {
        float targetFallSpeed;

        //Prevents character from falling at full fallspeed
        //when leaving the ground
        if (charController.isGrounded) targetFallSpeed = maxFallSpeed * 0.1f;
        else targetFallSpeed = maxFallSpeed;

        if (charController.isGrounded && isJumping)
        {
            yVelocity = jumpForce;
            animator.SetTrigger("jumpTrigger");
        }
        else if (yVelocity > -targetFallSpeed)
        {
            yVelocity -= gravity * Time.deltaTime;
        }
        else //Remain at fall speed limit
        {
            yVelocity = -targetFallSpeed;
        }

        isJumping = false;

        movement.y = yVelocity;
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
        isJumping = true;
    }

    public Vector3 GetVelocity()
    {
        return charController.velocity;
    }
}
