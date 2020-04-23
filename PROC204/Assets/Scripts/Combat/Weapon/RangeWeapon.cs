using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] Transform shootPosition;
    [SerializeField] Projectile projectile;
    [SerializeField] float reloadTime = 0.4f;
    [SerializeField] float inaccuracy = 0f;

    [Header("Burst Properties")]
    [SerializeField] bool isBurst;
    [SerializeField] int clipSize = 3;
    [SerializeField] float clipReloadTime = 3f;

    int currentClip;

    Vector3 targetPos;

    public override float AttackRate => reloadTime;

    private void Start()
    {
        currentClip = clipSize;
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public void Shoot()
    {
        Vector3 launchPos = new Vector3(shootPosition.position.x, shootPosition.position.y, 0f);
        Projectile instance = Instantiate(projectile, launchPos, Quaternion.identity);

        Vector3 targetDir = targetPos - launchPos;

        float randomAccuracy = Random.Range(-inaccuracy, inaccuracy);

        Vector2 aim = new Vector2(targetDir.x + randomAccuracy, targetDir.y  + randomAccuracy);

        instance.SetDirection(aim.normalized);        
    }

    public override void UseWeapon()
    {        
        if (!isReady) return;

        isReady = false;
        animator.SetTrigger("attackTrigger");

        float thisReload = reloadTime;
        if (isBurst)
        {
            currentClip--;

            if (currentClip < 1)
            {
                thisReload = clipReloadTime;
                currentClip = clipSize;
            }
        }

        Invoke(nameof(WeaponReady), thisReload);
    }
}
