using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] Projectile loadedProjectile;
    [SerializeField] Projectile secondLoadedProjectile;
    [SerializeField] Projectile thirdLoadedProjectile;
    [SerializeField] Projectile fourthLoadedProjectile;
    private int currentProjectile = 0;

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
        if (currentProjectile % 4 == 0)
        {
            currentProjectile = currentProjectile + 1;
            Projectile projectile = Instantiate(secondLoadedProjectile, transform.position, Quaternion.identity);
            projectile.AddForce(currentAimDirection);
            onFire?.Invoke();
            StartCoroutine(Reload());

        }
        else if (currentProjectile % 4 == 1)
        {
            currentProjectile = currentProjectile + 1;
            Projectile projectile = Instantiate(thirdLoadedProjectile, transform.position, Quaternion.identity);
            projectile.AddForce(currentAimDirection);

            onFire?.Invoke();
            StartCoroutine(Reload());
        }
        else if (currentProjectile % 4 == 2)
        {
            currentProjectile = currentProjectile + 1;
            Projectile projectile = Instantiate(fourthLoadedProjectile, transform.position, Quaternion.identity);
            projectile.AddForce(currentAimDirection);

            onFire?.Invoke();
            StartCoroutine(Reload());
        }
        else if (currentProjectile % 4 == 3)
        {
            currentProjectile = currentProjectile + 1;
            Projectile projectile = Instantiate(loadedProjectile, transform.position, Quaternion.identity);
            projectile.AddForce(currentAimDirection);

            onFire?.Invoke();
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(loadedProjectile.ReloadTime);
        isReloaded = true;
    }
}
