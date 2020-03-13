using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50; //Serialise so you can edit in editor
    Animator animator;
    string objectName;
    bool isDead = false;
    public HealthUI healthScript; //Used for invoking healthUI update

    private void Awake()
    {

        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        objectName = this.gameObject.name;

    }

    public void DealDamage(int damage)
    {
        if (objectName == "Cardinal")
        {
            healthScript.TakeHealthUpdate(); //Updates the health UI
        }

        if (isDead) return;

        health -= damage;

        if (health < 1) Die();
    }

    private void Die()
    {
        isDead = true;

        gameObject.layer = LayerMask.NameToLayer("Passable");
        animator.SetTrigger("Die");
    }
}
