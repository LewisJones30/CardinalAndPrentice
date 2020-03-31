using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] Transform shootPosition;
    [SerializeField] Projectile projectile;

    Vector2 currentAim;

    public override float AttackRate => projectile.ReloadTime;

    public void SetAim(Vector2 aim)
    {
        currentAim = aim;
    }
    public void Shoot()
    {
        Projectile instance = Instantiate(projectile, shootPosition.position, Quaternion.identity);
        instance.SetDirection(currentAim);
    }
}
