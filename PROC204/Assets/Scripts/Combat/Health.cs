using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    public int health = 50;
    [SerializeField] HealthUI healthUI;
    [SerializeField] bool isDestroyed = false;
    [SerializeField] float destroyDelay = 3f;

    Animator animator;
    ParticleSystem sleepParticle;
    ComboSystem CardinalCombo;

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

        if (this.gameObject.name == "Cardinal")
        {
            CardinalCombo = GetComponent<ComboSystem>();
        }
    }

    public void DealDamage(int damage)
    {
        if (IsDead) return;

        health -= damage;

        if (gameObject.tag == "Player 1")
        {
            healthUI.TakeHealthUpdate();
            CardinalCombo.decreaseDamage();
        }

        onHealthChange?.Invoke();
        if (health < 1) Die();        
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

        if (animator != null) animator.SetTrigger("Die");
        if (sleepParticle != null) sleepParticle.Play();

        if (isDestroyed) Destroy(gameObject, destroyDelay);
    }

}
