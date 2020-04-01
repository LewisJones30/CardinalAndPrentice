using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] Transform shootPosition;
    [SerializeField] Projectile projectile;

    Vector3 targetPos;

    public override float AttackRate => projectile.ReloadTime;

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public void Shoot()
    {
        Vector3 launchPos = new Vector3(shootPosition.position.x, shootPosition.position.y, 0f);
        Projectile instance = Instantiate(projectile, launchPos, Quaternion.identity);

        Vector3 targetDir = targetPos - launchPos;
        Vector2 aim = new Vector2(targetDir.x, targetDir.y);

        instance.SetDirection(aim.normalized);
    }
}
