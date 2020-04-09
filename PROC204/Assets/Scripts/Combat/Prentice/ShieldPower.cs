using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    [SerializeField] Shield shieldPrefab;
    [SerializeField] float shieldDistance = 3f;
    [SerializeField] float shieldDelayAfterWeaponFire = 2f;
    [SerializeField] float shieldRefresh = 0.5f;

    Shield currentShield;
    Transform cardinal;
    PrenticeAttack rangedWeapon;

    float timeSinceWeaponFire = Mathf.Infinity;
    float timeSinceShieldDestroyed = Mathf.Infinity;

    private void Awake()
    {
        cardinal = GameObject.FindWithTag("Player 1").transform;
        rangedWeapon = GetComponent<PrenticeAttack>();
    }
    private void OnEnable()
    {
        rangedWeapon.onFire += ResetWeaponFireTimer;
    }

    private void OnDisable()
    {
        rangedWeapon.onFire -= ResetWeaponFireTimer;
    }

    public void Protect(Vector2 dir)
    {
        if (dir.Equals(Vector2.zero) || 
            timeSinceWeaponFire < shieldDelayAfterWeaponFire)
        {
            if (currentShield != null)
            {
                currentShield.onDestroy -= DestroyShield;
                Destroy(currentShield.gameObject);
            }
            return;
        }

        if (currentShield == null && timeSinceShieldDestroyed > shieldRefresh)
        {
            currentShield = Instantiate(shieldPrefab, cardinal);
            currentShield.onDestroy += DestroyShield;
        }

        PositionShield(dir);
    }

    private void PositionShield(Vector2 dir)
    {
        if (currentShield == null) return;

        Vector3 v3Dir = new Vector3(dir.x, dir.y, 0f);
        Vector3 shieldPos = cardinal.position + v3Dir * shieldDistance;
        currentShield.transform.position = shieldPos;
        currentShield.transform.right = dir;
    }
    
    private void ResetWeaponFireTimer()
    {
        timeSinceWeaponFire = 0f;
    }

    private void DestroyShield()
    {
        timeSinceShieldDestroyed = 0f;
        currentShield.onDestroy -= DestroyShield;
        Destroy(currentShield.gameObject);
    }

    private void Update()
    {
        timeSinceWeaponFire += Time.deltaTime;
        timeSinceShieldDestroyed += Time.deltaTime;
    }
}
