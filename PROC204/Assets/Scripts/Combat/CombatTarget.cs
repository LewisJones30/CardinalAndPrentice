using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTarget : MonoBehaviour
{
    [SerializeField] float parryCooldown = 1f;

    Mover mover;
    Health health;
    ComboSystem comboSystem;
    Animator animator;

    public ColourValue ColourWeakness { get; set; } = ColourValue.None;
    public bool IsParrying { get; set; } = false;

    Coroutine stunProgress;
    bool canParry = true;
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
        if (IsParrying)
        {
            float targetDirection = Mathf.Sign(attackPos.x - transform.position.x);
            if (targetDirection == mover.Direction) return false;
        }

        health.ChangeHealth(-damage);

        if (tag == "Player 1") comboSystem.BreakCombo();

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

    private void CheckStunned()
    {
        if (stunTimeRemaining > 0f)
        {
            stunTimeRemaining -= Time.deltaTime;
            if (!isStunned)
            {
                isStunned = true;
                BroadcastMessage("StunUpdate", true);
                animator.SetTrigger("stunTrigger");
                animator.SetBool("isStunned", true);
            }
        }
        else if (isStunned)
        {
            animator.SetBool("isStunned", false);
            BroadcastMessage("StunUpdate", false);
            isStunned = false;
        }
    }

    public void Stun(float duration)
    {
        stunTimeRemaining += duration;
    }

}
