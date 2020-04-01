using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] Transform shootPosition;
    [SerializeField] Projectile projectile;

    Vector2 currentAim;

    public override float AttackRate => projectile.ReloadTime;

    public void Aim(Vector3 aim)
    {
        currentAim = new Vector2(aim.x, aim.y);
        currentAim.Normalize();
    }

    public void Shoot()
    {
        Vector3 launchPos = new Vector3(shootPosition.position.x, shootPosition.position.y, 0f);
        Projectile instance = Instantiate(projectile, launchPos, Quaternion.identity);
        instance.SetDirection(currentAim);
    }
}
