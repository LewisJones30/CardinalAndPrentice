using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float patrolSpeedFraction = 0.2f;
    [SerializeField] float turnRefreshRate = 1f;

    Mover mover;

    float moveDirection = 1f;
    bool canTurn = true;

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        mover.Move(moveDirection * patrolSpeedFraction);
    }

    private void Update()
    {
        if (mover.GetVelocity().magnitude < 1f && canTurn)
        {
            canTurn = false;
            moveDirection *= -1;
            Invoke(nameof(RefreshTurn), turnRefreshRate);
        }
    }

    private void RefreshTurn()
    {
        canTurn = true;
    }
}
