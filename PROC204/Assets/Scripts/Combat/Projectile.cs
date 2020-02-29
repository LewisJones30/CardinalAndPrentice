using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxDistance = 1000f;
    [SerializeField] float reloadTime = 0.4f;
    public float ReloadTime => reloadTime;

    float distanceTravelled = 0f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.right * moveSpeed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        distanceTravelled = Time.deltaTime * moveSpeed;

        if (distanceTravelled > maxDistance) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
