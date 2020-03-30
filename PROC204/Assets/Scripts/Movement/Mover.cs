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

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        layerName = LayerMask.LayerToName(gameObject.layer);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        FallingAnimation(charController.isGrounded);
        RollInvulnerability();
        PassPlatforms();
        CalculateYVelocity();
    }

    private void PassPlatforms()
    {
        if (charController.velocity.y > 0 && !charController.isGrounded) Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
        else Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
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

        Vector3 movement = input * Vector3.right * moveSpeed;
        movement.y = yVelocity;

        charController.Move(movement * Time.deltaTime);

        UpdateAnimator(input);
    }

    private void CalculateYVelocity()
    {
        if (charController.isGrounded && isJumping)
        {
            yVelocity = jumpForce;
            animator.SetTrigger("jumpTrigger");
        }

        isJumping = false;

        if (yVelocity > -maxFallSpeed) yVelocity -= gravity * Time.deltaTime;

        print(yVelocity);
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
