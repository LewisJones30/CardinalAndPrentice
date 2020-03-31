using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] Weapon weaponPrefab;
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;

    ComboSystem comboSystem;
    Animator animator;
    bool attackReset = true;

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
        //INCREASE DAMAGE OF PRENTICE WHEN COMBO WITH CARDINAL
        comboSystem = GetComponent<ComboSystem>();
        animator = GetComponent<Animator>();

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
        if (!attackReset) return;

        attackReset = false;

        animator.SetTrigger("attackTrigger");

        Invoke(nameof(ResetAttack), weaponPrefab.AttackRate);
    }

    public void Aim(Vector2 input)
    {
        if (RangeWeapon != null) RangeWeapon.SetAim(input);
    }

    void ResetAttack()
    {
        attackReset = true;
    }  
    
    void Hit()
    {
        MeleeWeapon.Hit();
    }

    void Shoot()
    {
        RangeWeapon.Shoot();
    }
}
