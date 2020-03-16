﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Melee : MonoBehaviour
{
    [SerializeField] int damage = 20;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackHeight = 0.9f;
    [SerializeField] float attackRate = 1.2f;
    [SerializeField] int targetLayerIndex;

    bool attackReset = true;

    public Vector3 AttackPoint { get { return attackPoint.position; } }
    public Vector3 HitBox { get; private set; }

    public int TargetLayerIndex { get { return targetLayerIndex; } }
    public int TargetLayerMask { get { return 1 << targetLayerIndex; } }

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        HitBox = new Vector3(attackRange / 2, attackHeight / 2, 1f / 2);
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            attackPoint.position = transform.position + new Vector3(attackRange / 2, attackPoint.position.y, 0f);
        }        
    }

    public void Swing()
    {
        if (!attackReset) return;

        attackReset = false;

        animator.SetTrigger("attackTrigger");

        Invoke(nameof(ResetAttack), attackRate);
    }

    void ResetAttack()
    {
        attackReset = true;
    }

    void Hit()
    {
        Collider[] colliders = Physics.OverlapBox(attackPoint.position, HitBox, Quaternion.identity, TargetLayerMask);

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();

            if (health != null) health.DealDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, attackHeight, 1f));
    }

}
