using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    Rigidbody rb;
    Collider pCollider;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        pCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (rb.velocity.y > 0) pCollider.isTrigger = true;
        else pCollider.isTrigger = false;
    }
}
