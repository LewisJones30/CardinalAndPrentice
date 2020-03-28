using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    Mover mover;
    Collider pCollider;

    private void Awake()
    {
        mover = GetComponentInParent<Mover>();
        pCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (mover.GetVelocity().y > 0) pCollider.enabled = true;
        else pCollider.enabled = false;
    }
}
