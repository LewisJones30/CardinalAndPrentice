using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenticeAttack : MonoBehaviour
{
    public PrenticeProjectile yellowProjectile;
    public PrenticeProjectile redProjectile;
    public PrenticeProjectile greenProjectile;
    public PrenticeProjectile blueProjectile;

    PrenticeProjectile loadedProjectile;

    bool isReloaded = true;
    Vector2 currentAimDirection;

    public delegate void OnFire();
    public event OnFire onFire;

    public int DamageIncrease { get; set; } = 0;

    public void Aim(Vector2 input)
    {
        currentAimDirection = input;

        if (currentAimDirection.Equals(Vector2.zero)) return;
        Debug.DrawRay(transform.position, input);
    }

    public void Fire(ColourValue colour)
    {
        ChangeProjectile(colour);

        if (!isReloaded || currentAimDirection.Equals(Vector2.zero)) return;
        isReloaded = false;

        Vector3 launchPos = new Vector3(transform.position.x, transform.position.y, 0f);
        PrenticeProjectile projectile = Instantiate(loadedProjectile, launchPos, Quaternion.identity);
        projectile.SetDirection(currentAimDirection);
        projectile.Damage += DamageIncrease;
        onFire?.Invoke();
        StartCoroutine(Reload());
    }

    private void ChangeProjectile(ColourValue colour)
    {
        switch (colour)
        {
            case ColourValue.Blue:
                loadedProjectile = blueProjectile;
                break;
            case ColourValue.Green:
                loadedProjectile = greenProjectile;
                break;
            case ColourValue.Red:
                loadedProjectile = redProjectile;
                break;
            case ColourValue.Yellow:
                loadedProjectile = yellowProjectile;
                break;
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(loadedProjectile.ReloadTime);
        isReloaded = true;
    }
}
