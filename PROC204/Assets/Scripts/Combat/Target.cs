using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Health
{
    [SerializeField] GameObject brokenVariant;
    [SerializeField] float minExplosionForce = 2000f;
    [SerializeField] float maxExplosionForce = 4000f;
    [SerializeField] float explosionRadius = 4f;

    Transform playerCamera;

    private void Awake()
    {
        playerCamera = FindObjectOfType<CinemachineBrain>().transform;
    }

    private void Update()
    {
        Vector3 camPos = new Vector3(playerCamera.position.x, transform.position.y, playerCamera.position.z);
        transform.LookAt(camPos);
    }

    protected override void Die()
    {
        base.Die();

        GameObject instance = Instantiate(brokenVariant, transform.position, transform.rotation);

        Rigidbody[] rigidbodies = instance.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in rigidbodies)
        {
            float explosionForce = Random.Range(minExplosionForce, maxExplosionForce);
            Vector3 explosionPos = transform.position + transform.forward;
            rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
        }

        Destroy(instance, 5f);
        Destroy(gameObject);
    }
}
