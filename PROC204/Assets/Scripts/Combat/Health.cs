using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] HealthUI healthUI;

    Animator animator;
    ParticleSystem sleepParticle;
    public bool IsDead { get; private set; } = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sleepParticle = GetComponent<ParticleSystem>();
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

        if (gameObject.name == "Cardinal")
        {
            healthUI.TakeHealthUpdate();
        }

        sleepParticle.Play();
    }
}
