﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenticeProjectile : Projectile
{    
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] ColourValue projectileColour;
    [SerializeField] GameObject projectileFiredFX;
    [SerializeField] GameObject projectileHitFX;
    [SerializeField] float seekingDistance = 20f;

    Collider targetEnemy;

    private void Start()
    {
        GameObject instance = Instantiate(projectileFiredFX, transform.position, Quaternion.identity);
        Destroy(instance, 10f);
    }


    protected override void Update()
    {
        base.Update();

        FindClosestTarget();
        SteerTowardTarget();
    }


    private void FindClosestTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, seekingDistance, LayerMask.GetMask("Enemy", "Target"));

        float smallestDistance = Mathf.Infinity;
        Collider closestEnemy = null;

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            if (health == null || health.IsDead) continue;

            float distance = Vector3.Distance(collider.transform.position, transform.position);

            if (distance < smallestDistance)
            {
                closestEnemy = collider;
                smallestDistance = distance;
            }
        }

        targetEnemy = closestEnemy;
    }
    private void SteerTowardTarget()
    {
        if (targetEnemy == null) return;

        Vector3 targetDir = targetEnemy.bounds.center - transform.position;
        transform.forward = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * Time.deltaTime, 0f);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health != null) health.DealDamage(damage, projectileColour);

        GameObject instance = Instantiate(projectileHitFX, transform.position, Quaternion.identity);
        Destroy(instance, 10f);

        Destroy(gameObject);
    }

}
