using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] Weapon weaponPrefab;
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] float parryCooldown = 1f;

    ComboSystem comboSystem;
    Animator animator;
    Health health;
    Mover mover;

    public ColourValue ColourWeakness { get; set; } = ColourValue.None;
    const int weaknessDamageMultiplier = 3;

    public int TargetLayerIndex { get; private set; }
    public int TargetLayerMask { get { return 1 << TargetLayerIndex; } }
    public bool IsMelee { get
        {
            if (weaponPrefab is MeleeWeapon) return true;
            else return false;
        }
    }

    bool canParry = true;
    public bool IsParrying { get; set; } = false;

    public bool IsAttackReady { get; private set; } = true;
    public MeleeWeapon MeleeWeapon { get; private set; }
    public RangeWeapon RangeWeapon { get; private set; }

    private void Awake()
    {
        comboSystem = GetComponent<ComboSystem>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        mover = GetComponent<Mover>();

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
        if (!IsAttackReady) return;

        IsAttackReady = false;

        animator.SetTrigger("attackTrigger");

        Invoke(nameof(ReadyAttack), weaponPrefab.AttackRate);
    }

    public void TakeDamage(int damage, ColourValue colour, Vector3 attackPos)
    {
        if (ColourWeakness == colour) damage *= weaknessDamageMultiplier;

        TakeDamage(damage, attackPos);
    }

    public void TakeDamage(int damage, Vector3 attackPos)
    {
        float targetDirection = Mathf.Sign(attackPos.x - transform.position.x);

        if (targetDirection == mover.Direction && IsParrying) return;

        health.ChangeHealth(-damage);

        if (tag == "Player 1") comboSystem.BreakCombo();
    }

    public void Parry()
    {
        if (!canParry) return;

        canParry = false;
        animator.SetTrigger("parryTrigger");
        Invoke(nameof(ResetParry), parryCooldown);
    }

    private void ResetParry()
    {
        canParry = true;
    }

    void ReadyAttack()
    {
        IsAttackReady = true;
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
