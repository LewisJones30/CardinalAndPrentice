using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    Animator animator;

    public bool IsDead { get; private set; } = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DealDamage(int damage)
    {
        if (IsDead) return;

        health -= damage;

        if (health < 1) Die();
    }

    private void Die()
    {
        IsDead = true;

        gameObject.layer = LayerMask.NameToLayer("Passable");
        animator.SetTrigger("Die");
    }
}
