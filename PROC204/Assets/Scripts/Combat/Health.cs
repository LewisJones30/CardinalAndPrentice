using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    Animator animator;

    ParticleSystem sleepParticle;

    bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sleepParticle = GetComponent<ParticleSystem>();
    }

    public void DealDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health < 1) Die();
    }

    private void Die()
    {
        isDead = true;

        gameObject.layer = LayerMask.NameToLayer("Passable");
        animator.SetTrigger("Die");

        sleepParticle.Play();
    }
}
