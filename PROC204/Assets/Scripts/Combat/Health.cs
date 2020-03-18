using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] HealthUI healthUI;

    Animator animator;
    ParticleSystem sleepParticle;
    bool playerDead = false;
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

        if (gameObject.name == "Cardinal")
        {
            healthUI.TakeHealthUpdate();
        }

        if (health < 1) Die();
    }

    private void Die()
    {
        IsDead = true;
        if (this.gameObject.name == "Cardinal")
        {
            healthUI.gameOver();
        }
        gameObject.layer = LayerMask.NameToLayer("Passable");
        animator.SetTrigger("Die");

        sleepParticle.Play();
    }

}
