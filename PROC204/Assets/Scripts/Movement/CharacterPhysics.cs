using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    [SerializeField] bool isFlying = false;
    [SerializeField] float gravity = 80f;
    [SerializeField] float maxFallSpeed = 50f;
    [SerializeField] float fallingAnimationDelay = 0.2f;
    [Range(0f, 0.95f)]
    [SerializeField] float horizontalDrag = 0.1f;
    [Range(0f, 1f)]
    [SerializeField] float groundFriction = 0.9f;
    [SerializeField] float knockBackResistance = 20f;
    [SerializeField] float knockBackDecay = 0.8f;

    [Header("Sound FX")]
    [SerializeField] RandomAudioPlayer landedPlayer;

    //CACHE REFERENCES

    CharacterController charController;
    CombatTarget combatTarget;
    Animator animator;

    //STATES

    //time since touching the ground
    float airTime = 0f;

    //Time until character can be controlled
    //Otherwise character affected by knockback forces
    float knockOutTimeRemaining = 0f;

    //Prevents head jumping
    bool isSlipping = false;

    // Stores desired movement for that frame depending
    //on controller input
    Vector3 desiredMovement;

    //Stored movement values effected by drag and gravity
    Vector2 characterVelocity;

    // Blocks player movement during knockback
    Coroutine knockBackProgress;

    //PROPERTIES

    public bool IsStuck { get; private set; } = false;

    //Is true when the character is in the
    //air because of jumping
    public bool HasJumped { get; private set; } = false;

    //Knockback ID prevents a knockback force from an entity
    //from being passed on to another entity more than once
    public float KnockBackID { get; private set; }

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        combatTarget = GetComponent<CombatTarget>();
        animator = GetComponent<Animator>();
    }

    //Ensures all input is taken from Update() and then movement
    //is applied afterwards
    private void LateUpdate()
    {
        ApplyDesiredCharacterMovement();
        CheckFalling();

        //Keeps entity on the Z axis on 2D sideon environment
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    //When entity is not knockedback they are free to move in any direction they want
    //at a constant horizontal speed
    private void ApplyDesiredCharacterMovement()
    {
        if (knockBackProgress != null) return;

        ApplyGravity(); //Only external force that is applied when entity controls movement

        characterVelocity.y += desiredMovement.y; //Add flying force to overall character velocity
        desiredMovement.y = characterVelocity.y; //Vertical movement not directly controlled

        var flags = charController.Move(desiredMovement * Time.deltaTime);
        ProcessFlags(flags);

        if (!isSlipping) animator.SetFloat("forwardSpeed", Mathf.Abs(charController.velocity.x)); //animate movement
        else animator.SetFloat("forwardSpeed", 0f); //no movement animation when slipping

        desiredMovement = Vector3.zero; //reset input movement
    }

    //Character assumed stuck when collsion on the sides of character collider
    //Used by AI controller to traverse blocking obstacles or entities
    private void ProcessFlags(CollisionFlags flags)
    {
        if (flags.HasFlag(CollisionFlags.Sides)) IsStuck = true;
        else IsStuck = false;
    }

    //Knocks back this entity by set force and blocks input for set duration
    //ID prevents same knockback force from being applied more than once to this entity
    public void KnockBack(Vector3 force, float duration, float id)
    {
        KnockBackID = id;
        if (force.magnitude <= knockBackResistance) return; //Entity can ignore small knockback forces depending on their resistance

        if (knockBackProgress != null) StopCoroutine(knockBackProgress); //Reset knockback progress
        knockBackProgress = StartCoroutine(KnockBackProgress(force, duration)); //Apply knockback force for duration
        combatTarget.Stun(duration); //Block input by stun
    }

    //Runs for duration of set knockback time while input is blocked
    IEnumerator KnockBackProgress(Vector3 force, float duration)
    {
        characterVelocity = force; //Immediately apply full knockback force

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            knockOutTimeRemaining = duration - time;

            ApplyGravity();
            ApplyDrag(); //Slows horizontal knockback force overtime

            charController.Move(characterVelocity * Time.deltaTime);

            yield return null;
        }

        desiredMovement = Vector3.zero; //Ensures input remains blocked and reset

        knockBackProgress = null; //Knockback progress finished
    }

    //Only used during knockback to slow horizontal force
    private void ApplyDrag()
    {
        if (isSlipping) return; //Slip force must remain constant
        characterVelocity.x *= 1 - horizontalDrag;
        if (charController.isGrounded) characterVelocity.x *= 1 - groundFriction; //Character slows significantly faster when touching ground
    }

    //Sets input from mover
    //Cannot take input when character knockedback or slipping
    public bool EntityMove(Vector3 movement)
    {
        if (knockBackProgress != null || isSlipping) return false;

        desiredMovement = movement;
        if (!isFlying) desiredMovement.y = 0f; //Only move veritcally when flying
        
        return true;
    }    

    //Called when character is on head of another character
    //Prevents sustained standing on other character heads
    public void SlipMove(float horizontalForce)
    {
        isSlipping = true;
        desiredMovement.x = horizontalForce;
    }

    //Input is allowed to be taken again
    public void FinishSlip()
    {
        isSlipping = false;
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

    //Can only jump when touching ground
    public bool Jump(float jumpForce)
    {
        if (!charController.isGrounded && airTime > fallingAnimationDelay) return false; 

        HasJumped = true;            
        characterVelocity.y = jumpForce;
        return true;
    }

    //Sets fall animation when in the air
    private void CheckFalling()
    {
        if (charController.isGrounded)
        {
            if (HasJumped) landedPlayer.PlayRandomAudio(); //Play land on ground sound FX
            airTime = 0f;
            HasJumped = false;
        }
        else airTime += Time.deltaTime;

        //Prevents fall animation from playing if air time is below a certain threshold
        bool isFalling;
        if (!charController.isGrounded && airTime > fallingAnimationDelay) isFalling = true;
        else isFalling = false;

        animator.SetBool("isGrounded", !isFalling);
    }

    //Applies chain knockback to other characters this character touches
    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (knockBackProgress == null) return;
        CharacterPhysics charPhysics = other.gameObject.GetComponent<CharacterPhysics>();
        if (charPhysics == null || charPhysics.KnockBackID == KnockBackID) return;

        //Knockback decay prevents infinite chain of knockback
        charPhysics.KnockBack(characterVelocity * knockBackDecay, knockOutTimeRemaining * knockBackDecay, KnockBackID);
    }

}
