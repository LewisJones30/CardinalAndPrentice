using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float patrolSpeedFraction = 0.6f;
    [SerializeField] float pursueSpeedFraction = 0.8f;
    [SerializeField] float turnRefreshRate = 1f;
    [SerializeField] float findPlayerRate = 0.5f;
    [SerializeField] float lineOfSight = 5f;
    [SerializeField] LayerMask targetMask;

    Transform currentTarget;
    float speedFraction = 1f;

    Mover mover;

    float moveDirection = 1f;
    bool canTurn = true;

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void Start()
    {
        StartCoroutine(FindPlayer());
    }

    private void FixedUpdate()
    {
        mover.Move(moveDirection * speedFraction);
    }

    IEnumerator FindPlayer()
    {
        while(true)
        {
            if (currentTarget == null) speedFraction = patrolSpeedFraction;
            else speedFraction = pursueSpeedFraction;

            yield return new WaitForSeconds(findPlayerRate);

            if (currentTarget != null)
            {
                float targetDistance = Vector3.Distance(transform.position, currentTarget.position);

                if (targetDistance > lineOfSight) currentTarget = null;
                else continue;
            }

            Collider[] targets = Physics.OverlapSphere(transform.position, lineOfSight, targetMask);

            if (targets.Length < 1) continue;

            currentTarget = targets[0].transform;
        }        
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            float targetDirection = Mathf.Sign(currentTarget.position.x - transform.position.x);

            if (targetDirection != moveDirection) Turn();
        }

        if (mover.GetVelocity().magnitude < 1f && canTurn)
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
