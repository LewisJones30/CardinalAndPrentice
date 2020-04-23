using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] Weapon weaponPrefab;
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] float attackAnimationSpeedMult = 1f;

    Health health;

    public int TargetLayerIndex { get; private set; }
    public int TargetLayerMask { get { return 1 << TargetLayerIndex; } }
    public bool IsMelee { get
        {
            if (weaponPrefab is MeleeWeapon) return true;
            else return false;
        }
    }

    public MeleeWeapon MeleeWeapon { get; private set; }
    public RangeWeapon RangeWeapon { get; private set; }

    private void Awake()
    {
        health = GetComponent<Health>();

        GetComponent<Animator>().SetFloat("attackSpeed", attackAnimationSpeedMult);

        Setup();
    }    

    private void Setup()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");

        if (gameObject.layer == playerLayer) TargetLayerIndex = enemyLayer;
        else TargetLayerIndex = playerLayer;

        AttachWeapon();
    }

    private void AttachWeapon()
    {
        Transform weaponParent;
        if (weaponPrefab.IsRightHanded) weaponParent = rightHandTransform;
        else weaponParent = leftHandTransform;

        Weapon weaponInstance = Instantiate(weaponPrefab, weaponParent);
        weaponInstance.TargetLayerMask = TargetLayerMask;

        if (weaponInstance is MeleeWeapon) MeleeWeapon = (MeleeWeapon)weaponInstance;
        else RangeWeapon = (RangeWeapon)weaponInstance;
    }

    public void Attack()
    {
        if (IsMelee) MeleeWeapon.UseWeapon();
        else RangeWeapon.UseWeapon();
    }
    
    void Hit()
    {
        if (health.IsDead) return;
        MeleeWeapon.Hit();
    }

    void Shoot()
    {
        if (health.IsDead) return;
        RangeWeapon.Shoot();
    }
}
