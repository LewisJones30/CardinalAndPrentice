using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform characterBody;
    [SerializeField] float rollCooldown = 1.5f;
    [SerializeField] float dashSpeedMultiplier = 2f;
    [SerializeField] float dashAirSpeedMultiplier = 3f;

    public float Direction { get; private set; } = 1;
    public Vector3 Position { get => transform.TransformPoint(charController.center); }
    public bool IsDashing { get; set; } = false;
    public bool IsStuck { get => charPhysics.IsStuck; }
    public float MaxSpeed { get => runSpeed; }
    
    Animator animator;
    CharacterController charController;
    CharacterPhysics charPhysics;
    Health health;

    bool isJumpInput = false;
    bool canRoll = true;

    string layerName;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        charPhysics = GetComponent<CharacterPhysics>();

        layerName = LayerMask.LayerToName(gameObject.layer);
    }

    private void Update()
    {
        PassPlatforms();
    }

    private void PassPlatforms()
    {
        bool canPass;

        //Can pass through platform when travelling up and not touching the ground
        if (tag == "Player 1" && charController.velocity.y > 0 && !charController.isGrounded) canPass = true;
        else canPass = false;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer(layerName), LayerMask.NameToLayer("Platform"), canPass);
    }

    public void SetRolling(bool isRolling)
    {
        if (isRolling) gameObject.layer = LayerMask.NameToLayer("Passable");
        else if (!health.IsDead) gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    public void Move(float input, float speedFraction)
    {
        if (Mathf.Abs(input) < Mathf.Epsilon) return;

        float moveSpeed = runSpeed * speedFraction;

        if (IsDashing) input = Mathf.Sign(input);
        
        if (IsDashing && charPhysics.HasJumped) moveSpeed *= dashAirSpeedMultiplier;
        else if (IsDashing) moveSpeed *= dashSpeedMultiplier;

        Vector3 movement = input * Vector3.right * moveSpeed;
        charPhysics.PlayerMove(movement);

        Turn(input);
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

    public void Turn(float input)
    {
        if (Mathf.Abs(input) < Mathf.Epsilon) return;

        if (input > 0) Direction = 1;
        else if (input < 0) Direction = -1;

        characterBody.forward = new Vector3(Direction, 0, 0);
    }    

    // If jump successful add up force to character
    // and play the jump animation
    public void Jump()
    {
        if (charPhysics.Jump(jumpForce)) animator.SetTrigger("jumpTrigger");
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
