using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float patrolSpeedFraction = 0.6f;
    [SerializeField] float pursueSpeedFraction = 0.8f;
    [SerializeField] float turnRefreshRate = 1f;
    [SerializeField] float reactionTime = 0.5f;
    [SerializeField] float lineOfSight = 5f;

    Health currentTarget;
    float speedFraction = 1f;

    Mover mover;
    Melee melee;
    AttackCollider attackCollider;
    Health health;

    float moveDirection = 1f;
    bool canTurn = true;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        melee = GetComponent<Melee>();
        health = GetComponent<Health>();
        attackCollider = GetComponentInChildren<AttackCollider>();
    }

    private void Start()
    {
        StartCoroutine(AIBrain());
    }

    private void FixedUpdate()
    {
        if (health.IsDead) return;

        if (!attackCollider.CanAttack) mover.Move(moveDirection, speedFraction);
    }
    IEnumerator AIBrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(reactionTime);

            if (health.IsDead) continue;

            FindTarget();
            Attack();
        }
    }

    private void FindTarget()
    {
        if (currentTarget == null) speedFraction = patrolSpeedFraction;
        else speedFraction = pursueSpeedFraction;

        if (currentTarget != null)
        {
            float targetDistance = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (targetDistance > lineOfSight || currentTarget.IsDead) currentTarget = null;
            else return;
        }

        Collider[] targets = Physics.OverlapSphere(transform.position, lineOfSight, melee.TargetLayerMask);

        if (targets.Length < 1) return;

        currentTarget = targets[0].gameObject.GetComponent<Health>();
    }


    void Attack()
    {
        if (!attackCollider.CanAttack) return;

        melee.Swing();
    }

    private void Update()
    {
        if (health.IsDead) return;

        if (currentTarget != null)
        {
            float targetDirection = Mathf.Sign(currentTarget.transform.position.x - transform.position.x);

            if (targetDirection != moveDirection && canTurn) Turn();
        }
        else if (mover.GetVelocity().magnitude < 1f && canTurn)
        {
            Turn();
        }
    }

    private void Turn()
    {
        canTurn = false;
        moveDirection *= -1;
        Invoke(nameof(RefreshTurn), turnRefreshRate);
    }

    private void RefreshTurn()
    {
        canTurn = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
