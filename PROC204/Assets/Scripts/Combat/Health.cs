using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    //CACHE REFERENCES

    Animator animator;
    ParticleSystem sleepParticle;

    public delegate void OnHealthChange();
    public event OnHealthChange onHealthChange;

    //PROPERTIES

    public bool IsDead { get; private set; } = false;
    public int HealthPoints { get { return health; } }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        sleepParticle = GetComponent<ParticleSystem>();
    }

    //Change in health can be positive or negative
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

        //Notify controllers to block inputs from player or AI
        BroadcastMessage("DeadUpdate", true, SendMessageOptions.DontRequireReceiver); 

        //Enable walking through dead entity colliders
        gameObject.layer = LayerMask.NameToLayer("Passable");

        if (animator != null)
        {
            animator.SetBool("isDead", true);
            animator.SetTrigger("Die");
        }

        if (sleepParticle != null) sleepParticle.Play(); //Sleeping VFX
    }

}
