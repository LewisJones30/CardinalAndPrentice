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
    [SerializeField] float dashAirSpeedMultiplier = 2.5f;
    [SerializeField] float normalAirSpeedMultiplier = 1.5f;
    [SerializeField] float flySpeed = 10f;
    [SerializeField] float rollSpeedMult = 1.2f;

    [Header("Sound FX")]
    [SerializeField] RandomAudioPlayer normalFootstepsPlayer;
    [SerializeField] RandomAudioPlayer runFootstepsPlayer;
    [SerializeField] RandomAudioPlayer rollPlayer;

    [Header("VFX")]
    [SerializeField] ParticleSystem[] runVFX;

    //PROPERTIES
    public float Direction { get; private set; } = 1;
    public Vector3 Position { get => transform.TransformPoint(charController.center); }
    public bool IsDashing { get; set; } = false;

    public bool IsRolling { get; set; } = false;
    public bool IsStuck { get => charPhysics.IsStuck; }
    public float MaxSpeed { get => runSpeed; }

    //CACHE REFERENCES    
    Animator animator;
    CharacterController charController;
    CharacterPhysics charPhysics;
    Health health;

    //STATES
    bool isJumpInput = false;
    bool canRoll = true;

    //Used when setting layer back to default 
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
        EnableRunVFX();
    }

    private void EnableRunVFX()
    {
        bool isRunning = IsDashing && charController.velocity.x != 0;

        foreach (var ps in runVFX)
        {
            var mod = ps.emission;
            mod.enabled = isRunning;
        }        
    }

    //Swaps layers so character can pass through platforms when jumping from under them
    //Reserved for Cardinal only
    private void PassPlatforms()
    {
        bool canPass;

        //Can pass through platform when travelling up and not touching the ground
        if (tag == "Player 1" && charController.velocity.y > 0 && !charController.isGrounded) canPass = true;
        else canPass = false;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer(layerName), LayerMask.NameToLayer("Platform"), canPass);
    }

    //Rolling allows character to pass through other character colliders
    //and dodge incoming projectiles
    public void SetRolling(bool isRolling)
    {
        IsRolling = isRolling;
        if (isRolling) gameObject.layer = LayerMask.NameToLayer("Passable");

        //Layer should remain passable if character dies
        else if (!health.IsDead) gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    public void Move(Vector3 input, float speedFraction)
    {
        if (Mathf.Abs(input.x) < Mathf.Epsilon && Mathf.Abs(input.y) < Mathf.Epsilon) return; //No input?

        float moveSpeed = runSpeed * speedFraction; //Speed fraction used by AI to set walk / run speed etc.

        if (IsDashing) input.x = Mathf.Sign(input.x); //Running speed is constant irrespective of smaller input values

        if (IsDashing && charPhysics.HasJumped) moveSpeed *= dashAirSpeedMultiplier; //Running and in the air
        else if (IsDashing) moveSpeed *= dashSpeedMultiplier; //Running on the ground
        else if (charPhysics.HasJumped) moveSpeed *= normalAirSpeedMultiplier; //Walking and in the air

        if (IsRolling) moveSpeed *= rollSpeedMult;

        input.x *= moveSpeed;
        input.y *= flySpeed;

        charPhysics.EntityMove(input);

        Turn(input.x);
    }    

    public void ForwardRoll()
    {
        if (!canRoll || !charController.isGrounded) return;

        canRoll = false;
        animator.SetTrigger("rollTrigger");
        rollPlayer.PlayRandomAudio();
        Invoke(nameof(EnableRoll), rollCooldown);
    }

    private void EnableRoll()
    {
        canRoll = true;
    }

    //Flip character model depeding on move direction
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
        if (charPhysics.Jump(jumpForce))
        {
            runFootstepsPlayer.PlayRandomAudio(); //Play jump sound
            animator.SetTrigger("jumpTrigger");
        }
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

    //Animation events to play step sound effects
    void Step()
    {
        if (IsDashing) return;
        normalFootstepsPlayer.PlayRandomAudio();
    }

    void RunStep()
    {
        if (!IsDashing) return;
        runFootstepsPlayer.PlayRandomAudio();
    }
}
