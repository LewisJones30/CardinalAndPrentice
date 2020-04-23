using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float maxDistance = 1000f;
    [SerializeField] GameObject projectileFiredFX;
    [SerializeField] protected GameObject projectileHitFX;

    float distanceTravelled = 0f;

    private void Start()
    {
        if (projectileFiredFX == null) return;

        GameObject instance = Instantiate(projectileFiredFX, transform.position, Quaternion.identity);
        Destroy(instance, 10f);
    }

    public void SetDirection(Vector2 dir)
    {
        transform.forward = dir;
    }

    protected virtual void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * moveSpeed, Space.World);

        distanceTravelled += Time.deltaTime * moveSpeed;
        if (distanceTravelled > maxDistance) Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        CombatTarget combatTarget = other.gameObject.GetComponent<CombatTarget>();
        if (combatTarget != null) combatTarget.TakeDamage(damage, transform.position);

        if (projectileHitFX != null)
        {
            GameObject instance = Instantiate(projectileHitFX, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
            Destroy(instance, 10f);
        }       

        Destroy(gameObject);
    }
}
