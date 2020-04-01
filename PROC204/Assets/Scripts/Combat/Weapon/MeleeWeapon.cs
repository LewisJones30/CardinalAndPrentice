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

    Mover mover;
    CharacterController charController;

    protected override void Awake()
    {
        base.Awake();

        mover = GetComponentInParent<Mover>();
        charController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        SlashEffect();
    }

    private void SlashEffect()
    {
        if (slashEffect == null) return;

        bool isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        var emission = slashEffect.emission;
        if (isAttacking) emission.enabled = true;
        else emission.enabled = false;
    }

    private Vector3 GetMeleeAttackCenter()
    {
        Vector3 offset = Vector3.right * mover.Direction * charController.radius;
        Vector3 attackStartPos =  charController.transform.TransformPoint(charController.center) + offset;

        return attackStartPos + (Vector3.right * mover.Direction * (attackRange / 2));
    }

    private Vector3 GetHitBox()
    {
        return new Vector3(attackRange / 2, charController.height / 2, 1f / 2);
    }

    public void Hit()
    {
        Collider[] colliders = Physics.OverlapBox(GetMeleeAttackCenter(), GetHitBox(), Quaternion.identity, TargetLayerMask);

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            if (health != null) health.DealDamage(damage);
        }
    }

    public bool CheckTargetInMeleeRange()
    {
        Collider[] colliders = Physics.OverlapBox(GetMeleeAttackCenter(), GetHitBox(), Quaternion.identity, TargetLayerMask);

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            if (health != null && !health.IsDead) return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (mover == null) return;
        Gizmos.DrawWireCube(GetMeleeAttackCenter(), GetHitBox() * 2);
    }
}
