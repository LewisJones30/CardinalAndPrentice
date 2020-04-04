using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float gravity = 40f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform characterBody;
    [SerializeField] float rollCooldown = 1.5f;
    [SerializeField] float maxFallSpeed = 100f;
    [SerializeField] float dashSpeedMultiplier = 2f;
    [SerializeField] float dashAirSpeedMultiplier = 3f;

    public float Direction { get; private set; } = 1;
    public Vector3 Position { get => transform.TransformPoint(charController.center); }
    public bool IsStuck { get; private set; } = false;
    public bool IsDashing { get; set; } = false;
    
    Animator animator;
    CharacterController charController;
    Health health;
    float yVelocity;

    bool isJumpInput = false;
    bool canRoll = true;
    string layerName;
    Vector3 movement = Vector3.zero;

    //Is true when the character is in the
    //air because of jumping
    public bool hasJumped = false;

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

        var flags = charController.Move(movement * Time.deltaTime);
        if (flags.HasFlag(CollisionFlags.Sides)) IsStuck = true;
        else IsStuck = false;

        movement = Vector3.zero;

        RollInvulnerability();
        PassPlatforms();

        FallingAnimation(charController.isGrounded);
        MoveAnimation();

        if (charController.isGrounded) hasJumped = false;

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void PassPlatforms()
    {
        bool canPass;

        //Can pass through platform when travelling up and not touching the ground
        if (tag == "Player 1" && charController.velocity.y > 0 && !charController.isGrounded) canPass = true;
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
        float moveSpeed = runSpeed * speedFraction;

        if (IsDashing && charController.isGrounded) moveSpeed *= dashSpeedMultiplier;
        else if (IsDashing && hasJumped) moveSpeed *= dashAirSpeedMultiplier;

        movement = input * Vector3.right * moveSpeed;

        Turn(input);
    }

    private void CalculateYVelocity()
    {
        float targetFallSpeed;

        //Prevents character from falling at full fallspeed
        //when leaving the ground
        if (charController.isGrounded) targetFallSpeed = maxFallSpeed * 0.1f;
        else targetFallSpeed = maxFallSpeed;

        if (charController.isGrounded && isJumpInput)
        {
            yVelocity = jumpForce;
            hasJumped = true;
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

        isJumpInput = false;

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

    private void MoveAnimation()
    {
        animator.SetBool("isDashing", IsDashing);
        animator.SetFloat("forwardSpeed", Mathf.Abs(charController.velocity.x));
    }

    public void Turn(float input)
    {
        if (Mathf.Abs(input) < 0.1f) return;

        if (input > 0)
        {
            Direction = 1;
        }
        else if (input < 0)
        {
            Direction = -1;
        }

        characterBody.forward = new Vector3(Direction, 0, 0);
    }

    private void FallingAnimation(bool isGrounded)
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    public void Jump()
    {
        isJumpInput = true;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Portal")
        {
            GameObject complete = GameObject.Find("LevelComplete");
            LevelCompleteUI triggerLevelComplete = complete.GetComponent<LevelCompleteUI>();
            triggerLevelComplete.LevelComplete();
        }
    }
    public Vector3 GetVelocity()
    {
        return charController.velocity;
    }

    void FootL() { }
    void FootR() { }
}
