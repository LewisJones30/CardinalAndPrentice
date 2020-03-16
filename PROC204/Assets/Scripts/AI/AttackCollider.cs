using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    Melee melee;
    BoxCollider box;
    Health currentTarget;

    public bool CanAttack { get { return currentTarget != null; } }

    private void Awake()
    {
        melee = GetComponentInParent<Melee>();
        box = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        box.center = transform.InverseTransformPoint(melee.AttackPoint);
        box.size = melee.HitBox * 2;
    }

    private void Update()
    {
        if (currentTarget != null && currentTarget.IsDead)
        {
            currentTarget = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != melee.TargetLayerIndex) return;

        currentTarget = other.gameObject.GetComponent<Health>();

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != melee.TargetLayerIndex) return;

        currentTarget = null;
    }
}
