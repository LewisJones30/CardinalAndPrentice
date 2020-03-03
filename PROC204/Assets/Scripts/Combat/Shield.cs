using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] float shieldDistance = 3f;
    [SerializeField] float shieldDelayAfterWeaponFire = 2f;

    GameObject currentShield;
    Transform cardinal;
    RangedWeapon rangedWeapon;

    float timeSinceWeaponFire = 0f;

    private void Awake()
    {
        cardinal = GameObject.FindWithTag("Player 1").transform;
        rangedWeapon = GetComponent<RangedWeapon>();
    }
    private void OnEnable()
    {
        rangedWeapon.onFire += ResetTimer;
    }

    private void OnDisable()
    {
        rangedWeapon.onFire -= ResetTimer;
    }

    public void Protect(Vector2 dir)
    {
        if (dir.Equals(Vector2.zero) || timeSinceWeaponFire < shieldDelayAfterWeaponFire)
        {
            if (currentShield != null) Destroy(currentShield);
            return;
        }

        if (currentShield == null)
        {
            currentShield = Instantiate(shieldPrefab, cardinal);
        }

        PositionShield(dir);
    }

    private void PositionShield(Vector2 dir)
    {
        Vector3 v3Dir = new Vector3(dir.x, dir.y, 0f);
        Vector3 shieldPos = cardinal.position + v3Dir * shieldDistance;
        currentShield.transform.position = shieldPos;
        currentShield.transform.right = dir;
    }

    private void ResetTimer()
    {
        timeSinceWeaponFire = 0f;
    }

    private void Update()
    {
        timeSinceWeaponFire += Time.deltaTime;
    }
}
