using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] Projectile loadedProjectile;

    bool isReloaded = true;
    Vector2 currentAimDirection;

    public delegate void OnFire();
    public event OnFire onFire;

    public void Aim(Vector2 input)
    {
        currentAimDirection = input;

        if (currentAimDirection.Equals(Vector2.zero)) return;
        Debug.DrawRay(transform.position, input);
    }

    public void Fire()
    {
        if (!isReloaded || currentAimDirection.Equals(Vector2.zero)) return;

        isReloaded = false;
        Projectile projectile = Instantiate(loadedProjectile, transform.position, Quaternion.identity);
        projectile.AddForce(currentAimDirection);

        onFire?.Invoke();
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(loadedProjectile.ReloadTime);
        isReloaded = true;
    }
}
