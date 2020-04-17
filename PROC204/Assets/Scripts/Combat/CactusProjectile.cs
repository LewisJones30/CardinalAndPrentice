﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusProjectile : PrenticeProjectile
{
    [SerializeField] GameObject cactusPrefab;

    private void Awake()
    {
        isHoming = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        CombatTarget combatTarget = other.gameObject.GetComponentInParent<CombatTarget>();
        if (combatTarget != null) combatTarget.TakeDamage(damage, projectileColour, transform.position);
        else SpawnCactus(other.ClosestPointOnBounds(transform.position));

        GameObject instance = Instantiate(projectileHitFX, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
        Destroy(instance, 10f);

        Destroy(gameObject);
    }

    private void SpawnCactus(Vector3 spawnPos)
    {
        Instantiate(cactusPrefab, spawnPos, Quaternion.identity);
    }
}
