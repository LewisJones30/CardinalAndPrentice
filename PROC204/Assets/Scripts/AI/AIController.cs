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
    [SerializeField] float maxWaitTime = 4f;

    Health currentTarget;
    float speedFraction = 1f;
    float preferredShootingRange;

    Mover mover;
    Fighter fighter;
    Health health;

    bool isStationary = false;
    float moveDir = 1f;
    bool canTurn = true;
    float waitTime = Mathf.Infinity;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        if (loseTargetDistance < spotTargetDistance) loseTargetDistance = spotTargetDistance;
        preferredShootingRange = UnityEngine.Random.Range(spotTargetDistance, loseTargetDistance);

        StartCoroutine(AIBrain());
    }

    IEnumerator AIBrain()
    {
        while (!health.IsDead)
        {
            ValidateTarget();
            FindTarget();            
            Movement();
            Attack();

            if (currentTarget == null && waitTime < maxWaitTime) isStationary = true;

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
        else if (Mathf.Abs(mover.GetVelocity().x) < 0.1f && canTurn && !isStationary)
        {
            Turn();
        }
    }

    private void FindTarget()
    {
        if (currentTarget != null) return;

        Collider[] targets = Physics.OverlapSphere(mover.Position, spotTargetDistance, fighter.TargetLayerMask);

        if (targets.Length < 1) return;

        foreach (var target in targets)
        {
            Health health = target.gameObject.GetComponentInParent<Health>();
            if (health != null && !health.IsDead)
            {
                currentTarget = health;
            }
        }
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
        bool isHit = Physics.Raycast(mover.Position, targetDir, out RaycastHit hit, preferredShootingRange, shootLayerMask);

        if (!isHit) return false;

        fighter.RangeWeapon.SetTarget(targetPos);
        fighter.Attack();

        return true;
    }


    private void Update()
    {
        waitTime += Time.deltaTime;

        if (health.IsDead) return;

        if (!isStationary) mover.Move(moveDir, speedFraction);
    }

    private void ValidateTarget()
    {
        if (currentTarget == null) return;
        
        float targetDistance = Vector3.Distance(transform.position, currentTarget.transform.position);

        if (targetDistance > loseTargetDistance || currentTarget.IsDead)
        {
            currentTarget = null;
            waitTime = 0f;
        }
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
