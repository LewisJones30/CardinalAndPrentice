using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float patrolSpeedFraction = 0.6f;
    [SerializeField] float pursueSpeedFraction = 0.8f;
    [SerializeField] float turnRefreshRate = 1f;
    [SerializeField] float reactionTime = 0.5f;
    [SerializeField] float spotTargetDistance = 5f;
    [SerializeField] float loseTargetDistance = 10f;

    Health currentTarget;
    float speedFraction = 1f;

    Mover mover;
    Fighter fighter;
    Health health;

    bool isStationary = false;
    float moveDir = 1f;
    bool canTurn = true;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        if (loseTargetDistance < spotTargetDistance) loseTargetDistance = spotTargetDistance;

        StartCoroutine(AIBrain());
    }

    IEnumerator AIBrain()
    {
        while (!health.IsDead)
        {
            FindTarget();
            Movement();
            Attack();

            yield return new WaitForSeconds(reactionTime);
        }
    }

    private void Movement()
    {
        if (currentTarget == null) speedFraction = patrolSpeedFraction;
        else speedFraction = pursueSpeedFraction;

        if (currentTarget != null)
        {
            float targetDirection = Mathf.Sign(currentTarget.transform.position.x - transform.position.x);

            if ((targetDirection != moveDir) && canTurn) Turn();
        }
        else if (mover.GetVelocity().x < 1f && canTurn)
        {
            Turn();
        }
    }

    private void FindTarget()
    {
        if (currentTarget != null) return;

        Collider[] targets = Physics.OverlapSphere(mover.Position, spotTargetDistance, fighter.TargetLayerMask);

        if (targets.Length < 1) return;

        currentTarget = targets[0].gameObject.GetComponent<Health>();
    }

    void Attack()
    {        
        if (fighter.IsMelee)
        {
            isStationary = MeleeAttack();
        }
        else
        {
            isStationary = RangeAttack();
        }
    }

    // Returns true when enemy is in range to hit the player
    private bool MeleeAttack()
    {
        if (!fighter.MeleeWeapon.CheckTargetInMeleeRange()) return false;

        fighter.Attack();
        return true;
    }

    // Returns true when player is in line of sight of enemy
    private bool RangeAttack()
    {
        if (currentTarget == null) return false;

        Vector3 targetPos = currentTarget.GetComponent<Mover>().Position;
        Vector3 targetDir = targetPos - mover.Position;

        int shootLayerMask = LayerMask.GetMask("Player", "Default");
        bool isHit = Physics.Raycast(mover.Position, targetDir, out RaycastHit hit, loseTargetDistance, shootLayerMask);

        if (!isHit) return false;

        fighter.RangeWeapon.Aim(targetDir);
        fighter.Attack();

        return true;
    }


    private void Update()
    {
        if (health.IsDead) return;

        ValidateTarget();

        if (!isStationary) mover.Move(moveDir, speedFraction);
    }

    private void ValidateTarget()
    {
        if (currentTarget == null) return;
        
        float targetDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        if (targetDistance > loseTargetDistance || currentTarget.IsDead) currentTarget = null;
    }

    private void Turn()
    {
        canTurn = false;
        moveDir *= -1;
        mover.Turn(moveDir);
        Invoke(nameof(RefreshTurn), turnRefreshRate);
    }

    private void RefreshTurn()
    {
        canTurn = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spotTargetDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loseTargetDistance);
    }
}
