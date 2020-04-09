using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    [SerializeField] float gravity = 80f;
    [SerializeField] float maxFallSpeed = 50f;
    [SerializeField] float fallingAnimationDelay = 0.2f;
    [Range(0f, 0.95f)]
    [SerializeField] float horizontalDrag = 0.1f;
    [Range(0f, 1f)]
    [SerializeField] float groundFriction = 0.9f;
    [SerializeField] float knockBackResistance = 20f;
    [SerializeField] float knockBackDecay = 0.8f;

    CharacterController charController;
    CombatTarget combatTarget;
    Animator animator;

    float airTime = 0f;
    float knockOutTimeRemaining = 0f;

    // Stores player movement for that frame
    Vector3 playerMovement;

    //Stored movement values effected by drag and gravity
    Vector2 characterVelocity;

    // Blocks player movement during knockback
    Coroutine knockBackProgress;
    Coroutine stunProgress;

    public bool IsStuck { get; private set; } = false;
    public bool IsStoodOn { get; private set; } = false;

    //Is true when the character is in the
    //air because of jumping
    public bool HasJumped { get; private set; } = false;

    public float KnockBackID { get; private set; }

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        combatTarget = GetComponent<CombatTarget>();
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        ApplyPlayerMovement();

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void Update()
    {
        CheckFalling();
    }


    private void ApplyPlayerMovement()
    {
        if (knockBackProgress != null) return;

        ApplyGravity();
        playerMovement.y = characterVelocity.y;

        var flags = charController.Move(playerMovement * Time.deltaTime);
        ProcessFlags(flags);

        playerMovement = Vector3.zero;
    }

    private void ProcessFlags(CollisionFlags flags)
    {
        if (flags.HasFlag(CollisionFlags.Sides)) IsStuck = true;
        else IsStuck = false;
        if (flags.HasFlag(CollisionFlags.Above)) IsStoodOn = true;
        else IsStoodOn = false;
    }

    public void KnockBack(Vector3 force, float duration, float id)
    {
        KnockBackID = id;
        if (force.magnitude <= knockBackResistance) return;

        if (knockBackProgress != null) StopCoroutine(knockBackProgress);
        knockBackProgress = StartCoroutine(KnockBackProgress(force, duration));
        combatTarget.Stun(duration);
    }

    IEnumerator KnockBackProgress(Vector3 force, float duration)
    {
        characterVelocity = force;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            knockOutTimeRemaining = duration - time;

            ApplyGravity();
            ApplyDrag();

            charController.Move(characterVelocity * Time.deltaTime);

            yield return null;
        }

        playerMovement = Vector3.zero;

        knockBackProgress = null;
    }

    private void ApplyDrag()
    {
        characterVelocity.x *= 1 - horizontalDrag;
        if (charController.isGrounded) characterVelocity.x *= 1 - groundFriction;
    }

    public void PlayerMove(Vector3 playerMovement)
    {
        if (knockBackProgress != null) return;
        this.playerMovement = playerMovement;
    }    

    // Applies gravity
    private void ApplyGravity()
    {
        float targetFallSpeed;

        //Prevents character from falling at full fallspeed
        //when leaving the ground
        if (charController.isGrounded) targetFallSpeed = maxFallSpeed * 0.1f; 
        else targetFallSpeed = maxFallSpeed;

        if (characterVelocity.y > -targetFallSpeed)
        {
            characterVelocity.y -= gravity * Time.deltaTime;
        }
        else //Remain at fall speed limit
        {
            characterVelocity.y = -targetFallSpeed;
        }
    }

    public bool Jump(float jumpForce)
    {
        if (!charController.isGrounded) return false;

        HasJumped = true;            
        characterVelocity.y = jumpForce;
        return true;
    }

    private void CheckFalling()
    {
        if (charController.isGrounded)
        {
            airTime = 0f;
            HasJumped = false;
        }
        else airTime += Time.deltaTime;

        bool isFalling;
        if (!charController.isGrounded && airTime > fallingAnimationDelay) isFalling = true;
        else isFalling = false;

        animator.SetBool("isGrounded", !isFalling);
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (knockBackProgress == null) return;
        CharacterPhysics charPhysics = other.gameObject.GetComponent<CharacterPhysics>();
        if (charPhysics == null || charPhysics.KnockBackID == KnockBackID) return;

        charPhysics.KnockBack(characterVelocity * knockBackDecay, knockOutTimeRemaining * knockBackDecay, KnockBackID);
    }

}
