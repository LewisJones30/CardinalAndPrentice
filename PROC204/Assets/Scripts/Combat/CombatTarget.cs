using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTarget : MonoBehaviour
{
    [SerializeField] float parryCooldown = 1f;

    //CACHE REFERENCES

    Mover mover;
    Health health;
    ComboSystem comboSystem;
    Animator animator;

    //PROPERTIES

    public ColourValue ColourWeakness { get; set; } = ColourValue.None;
    public bool IsParrying { get; set; } = false;
    public bool IsDead { get => health.IsDead; }

    //STATES

    //False when parry cooling down
    bool canParry = true;

    //Damage multiplier when hit by projectile of same colour
    const int weaknessDamageMultiplier = 3;

    float stunTimeRemaining = 0f;
    bool isStunned = false;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        health = GetComponent<Health>();
        comboSystem = GetComponent<ComboSystem>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        CheckStunned();
    }

    public bool TakeDamage(int damage, ColourValue colour, Vector3 attackPos)
    {
        if (ColourWeakness == colour) damage *= weaknessDamageMultiplier;

        return TakeDamage(damage, attackPos);
    }

    public bool TakeDamage(int damage, Vector3 attackPos)
    {
        if (IsParrying) //Parry successful if facing position of attack
        {
            float targetDirection = Mathf.Sign(attackPos.x - transform.position.x);
            if (targetDirection == mover.Direction) return false; //Attacker unsucessful
        }

        health.ChangeHealth(-damage);

        if (!IsDead) animator.SetTrigger("damagedTrigger");

        if (tag == "Player 1") comboSystem.BreakCombo(); //Combo for Cardinal broken when damaged

        return true;
    }
    public void Parry()
    {
        if (!canParry) return;

        canParry = false;
        animator.SetTrigger("parryTrigger");
        Invoke(nameof(ResetParry), parryCooldown);
    }

    private void ResetParry()
    {
        canParry = true;
    }

    //Stop or start stun based on stun time remaining
    private void CheckStunned()
    {
        if (stunTimeRemaining > 0f) //Remain stunned if stun time remaining
        {
            stunTimeRemaining -= Time.deltaTime;
            if (!isStunned) //set stunned state
            {
                isStunned = true;
                BroadcastMessage("StunUpdate", true); //Controllers notified and cannot move
                animator.SetTrigger("stunTrigger");
                animator.SetBool("isStunned", true);
            }
        }
        else if (isStunned) //Go back to normal
        {
            animator.SetBool("isStunned", false);
            BroadcastMessage("StunUpdate", false);
            isStunned = false;
        }
    }

    //Stun by adding stun time
    public void Stun(float duration)
    {
        stunTimeRemaining += duration;
    }

}
