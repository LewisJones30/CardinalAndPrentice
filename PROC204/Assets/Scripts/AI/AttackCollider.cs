using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    Melee melee;
    BoxCollider box;

    public bool CanAttack { get { return targets.Count > 0; } }

    List<Health> targets = new List<Health>();

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
        List<Health> targetsToRemove = new List<Health>();

        foreach (Health target in targets)
        {
            if (target == null || target.IsDead ||
                target.gameObject.layer != melee.TargetLayerIndex) targetsToRemove.Add(target);
        }

        foreach (Health target in targetsToRemove)
        {
            targets.Remove(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != melee.TargetLayerIndex) return;

        Health target = other.gameObject.GetComponent<Health>();

        if (!targets.Contains(target)) targets.Add(target);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != melee.TargetLayerIndex) return;

        Health target = other.gameObject.GetComponent<Health>();

        if (!targets.Contains(target)) targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != melee.TargetLayerIndex) return;

        Health target = other.gameObject.GetComponent<Health>();

        if (targets.Contains(target)) targets.Remove(target);
    }
}
