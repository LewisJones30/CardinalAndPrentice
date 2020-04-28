using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController weaponAnimatorOverride;
    [SerializeField] bool isRightHanded = true;

    //PROPERTIES
    public bool IsRightHanded { get => isRightHanded; }
    public int TargetLayerMask { get; set; }
    public abstract float AttackRate { get; }

    //STATES

    protected bool isReady = true;

    //CACHE REFERENCES

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInParent<Animator>();
        OverrideAnimation();
    }

    //Overrides default animations with new animations associated with the current weapon
    private void OverrideAnimation()
    {
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

    public abstract void UseWeapon();

    protected void WeaponReady()
    {
        isReady = true;
    }
}
