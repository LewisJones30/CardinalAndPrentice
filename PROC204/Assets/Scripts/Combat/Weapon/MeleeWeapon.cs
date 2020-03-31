using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] float attackRate;
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] ParticleSystem slashEffect;

    public override float AttackRate { get => attackRate; }

    Animator animator;
    CharacterController charController;

    Vector3 hitBox;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        charController = GetComponentInParent<CharacterController>();

        hitBox = new Vector3(attackRange / 2, charController.height / 2, 1f / 2);
    }

    private void Update()
    {
        SlashEffect();
    }

    private void SlashEffect()
    {
        bool isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        var emission = slashEffect.emission;
        if (isAttacking) emission.enabled = true;
        else emission.enabled = false;
    }

    private Vector3 GetMeleeAttackPosition()
    {
        return charController.center + (Vector3.right * (attackRange / 2));
    }

    public void Hit()
    {
        Collider[] colliders = Physics.OverlapBox(GetMeleeAttackPosition(), hitBox, Quaternion.identity, TargetLayerMask);

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            if (health != null) health.DealDamage(damage);
        }
    }

    public bool CheckTargetInMeleeRange()
    {
        Collider[] colliders = Physics.OverlapBox(GetMeleeAttackPosition(), hitBox, Quaternion.identity, TargetLayerMask);

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            if (health != null && !health.IsDead) return true;
        }
        return false;
    }
}
