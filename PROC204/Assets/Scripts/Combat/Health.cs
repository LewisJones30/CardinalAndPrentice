using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    Animator animator;
    ParticleSystem sleepParticle;

    public delegate void OnHealthChange();
    public event OnHealthChange onHealthChange;

    public bool IsDead { get; private set; } = false;
    public int HealthPoints { get { return health; } }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        sleepParticle = GetComponent<ParticleSystem>();
    }

    public void ChangeHealth(int value)
    {
        if (IsDead) return;

        health += value;
        
        onHealthChange?.Invoke();

        if (health < 1) Die();        
    }    

    protected virtual void Die()
    {
        IsDead = true;
        BroadcastMessage("DeadUpdate", true, SendMessageOptions.DontRequireReceiver);

        gameObject.layer = LayerMask.NameToLayer("Passable");

        if (animator != null) animator.SetTrigger("Die");
        if (sleepParticle != null) sleepParticle.Play();
    }

}
