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

    public delegate void OnHealthChange();
    public event OnHealthChange onHealthChange;

    public bool IsDead { get; private set; } = false;
    public int HealthPoints { get { return health; } }
    public ColourValue ColourWeakness { get; set; } = ColourValue.None;

    const int damageMultiplier = 3;

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

        onHealthChange?.Invoke();
    }

    public void DealDamage(int damage, ColourValue colour)
    {
        if (ColourWeakness == colour) damage *= damageMultiplier;

        DealDamage(damage);
    }

    private void Die()
    {
        IsDead = true;
        if (this.gameObject.name == "Cardinal")
        {
            healthUI.gameOver();
        }

        animator.SetTrigger("Die");

        sleepParticle.Play();
    }

}
