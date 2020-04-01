﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float maxDistance = 1000f;
    [SerializeField] protected float reloadTime = 0.4f;

    public int Damage { get => damage; set => damage = value; }
    public float ReloadTime => reloadTime;

    float distanceTravelled = 0f;

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

    protected virtual void OnCollisionEnter(Collision other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health != null) health.DealDamage(damage);

        Destroy(gameObject);
    }
}
