using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController weaponAnimatorOverride;
    [SerializeField] bool isRightHanded = true;
    public bool IsRightHanded { get => isRightHanded; }
    public int TargetLayerMask { get; set; }
    public abstract float AttackRate { get; }

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();

        var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        if (weaponAnimatorOverride != null)
        {
            animator.runtimeAnimatorController = weaponAnimatorOverride;
        }
        else if (overrideController != null)
        {
            animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
        }
    }
}
