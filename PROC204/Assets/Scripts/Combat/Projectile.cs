using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float maxDistance = 1000f;
    public float reloadTime = 0.4f;
    [SerializeField] ColourValue projectileColour;
    [SerializeField] GameObject projectileFiredFX;
    [SerializeField] GameObject projectileHitFX;
    [SerializeField] float seekingDistance = 20f;

    Collider targetEnemy;

    public float ReloadTime => reloadTime;

    float distanceTravelled = 0f;

    private void Start()
    {
        GameObject instance = Instantiate(projectileFiredFX, transform.position, Quaternion.identity);
        Destroy(instance, 10f);
    }

    public void AddForce(Vector2 dir)
    {
        transform.forward = dir;
    }

    private void Update()
    {
        distanceTravelled += Time.deltaTime * moveSpeed;

        transform.Translate(transform.forward * Time.deltaTime * moveSpeed, Space.World);

        FindClosestTarget();
        SteerTowardTarget();

        if (distanceTravelled > maxDistance) Destroy(gameObject);
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

    private void OnCollisionEnter(Collision other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health != null) health.DealDamage(damage, projectileColour);

        GameObject instance =Instantiate(projectileHitFX, transform.position, Quaternion.identity);
        Destroy(instance, 10f);

        Destroy(gameObject);
    }

}
