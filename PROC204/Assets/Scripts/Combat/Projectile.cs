using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxDistance = 1000f;
    [SerializeField] float reloadTime = 0.4f;
    [SerializeField] ColourValue projectileColour;
    [SerializeField] GameObject projectileFiredFX;
    [SerializeField] GameObject projectileHitFX;
    [SerializeField] float seekingDistance = 20f;

    Health targetEnemy;

    public float ReloadTime => reloadTime;

    float distanceTravelled = 0f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameObject instance = Instantiate(projectileFiredFX, transform.position, Quaternion.identity);
        Destroy(instance, 10f);
    }

    public void AddForce(Vector2 dir)
    {
        print(dir);
        transform.right = dir;
        print(transform.right);
    }

    private void Update()
    {
        distanceTravelled += Time.deltaTime * moveSpeed;

        transform.Translate(transform.right * Time.deltaTime * moveSpeed, Space.World);

        FindClosestTarget();
        SteerTowardTarget();

        if (distanceTravelled > maxDistance) Destroy(gameObject);
    }


    private void FindClosestTarget()
    {
        if (targetEnemy != null) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, seekingDistance, LayerMask.GetMask("Enemy"));

        float smallestDistance = Mathf.Infinity;
        Health closestEnemy = null;

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            if (health == null) continue;

            float distance = Vector3.Distance(collider.transform.position, transform.position);

            if (distance < smallestDistance)
            {
                closestEnemy = health;
                smallestDistance = distance;
            }
        }

        targetEnemy = closestEnemy;
    }
    private void SteerTowardTarget()
    {
        if (targetEnemy == null) return;


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
