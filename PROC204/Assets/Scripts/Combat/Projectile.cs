using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxDistance = 1000f;
    public float reloadTime = 0.4f;
    [SerializeField] ColourValue projectileColour;

    public float ReloadTime => reloadTime;

    float distanceTravelled = 0f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AddForce(Vector2 dir)
    {
        transform.right = dir;
        rb.AddForce(transform.right * moveSpeed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        distanceTravelled += Time.deltaTime * moveSpeed;

        if (distanceTravelled > maxDistance) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health != null) health.DealDamage(damage, projectileColour);

        Destroy(gameObject);
    }

}
