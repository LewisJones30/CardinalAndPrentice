using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    [SerializeField] Shield shieldPrefab;
    [SerializeField] GameObject aimLine;
    [SerializeField] float shieldDistance = 3f;
    [SerializeField] float shieldDelayAfterWeaponFire = 2f;
    [SerializeField] float shieldRefresh = 0.5f;
    [SerializeField] RandomAudioPlayer destroyShieldPlayer;

    //CACHE REFERENCES

    Shield currentShield;
    GameObject currentAimLine;
    Mover cardinal;
    PrenticeAttack rangedWeapon;

    //STATES

    //Determines shield refresh
    float timeSinceWeaponFire = Mathf.Infinity;
    float timeSinceShieldDestroyed = Mathf.Infinity;

    private void Awake()
    {
        cardinal = GameObject.FindWithTag("Player 1").GetComponent<Mover>();
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

    //Put up shield
    public void Protect(Vector2 dir)
    {
        DisplayAim(dir); 

        if (dir.Equals(Vector2.zero) || 
            timeSinceWeaponFire < shieldDelayAfterWeaponFire) //Shield removed when not aiming or recently fired
        {
            if (currentShield != null) 
            {
                currentShield.onDestroy -= DestroyShield;
                Destroy(currentShield.gameObject);
            }
            return;
        }

        if (currentShield == null && timeSinceShieldDestroyed > shieldRefresh) // Instantiate shield
        {
            currentShield = Instantiate(shieldPrefab, cardinal.transform);
            currentShield.onDestroy += DestroyShield; //Track if shield is destroyed by touching enemy
        }

        PositionShield(dir);
    }

    //Displays faint aim line
    private void DisplayAim(Vector2 dir)
    {
        if (dir.Equals(Vector2.zero) || currentShield != null) //Remove if shield present or not aiming
        {
            if (currentAimLine != null) Destroy(currentAimLine);
        }
        else
        {
            if (currentAimLine == null)
            {
                currentAimLine = Instantiate(aimLine, cardinal.transform);
            }
        }

        PositionAimLine(dir);
    }

    //Position current aim line (same as shield)
    private void PositionAimLine(Vector2 dir)
    {
        if (currentAimLine == null) return;

        Vector3 v3Dir = new Vector3(dir.x, dir.y, 0f);
        Vector3 linePos = transform.position + v3Dir * shieldDistance;
        currentAimLine.transform.position = linePos;
        currentAimLine.transform.right = dir;
    }

    //Positions current shield 
    private void PositionShield(Vector2 dir)
    {
        if (currentShield == null) return;

        Vector3 v3Dir = new Vector3(dir.x, dir.y, 0f); 
        Vector3 shieldPos = cardinal.Position + v3Dir * shieldDistance; 
        currentShield.transform.position = shieldPos; //Position shield in pointed direction
        currentShield.transform.right = dir; //Rotate shield to face pointed direction
    }
    
    //Called when event for weapon fire triggered
    private void ResetWeaponFireTimer()
    {
        timeSinceWeaponFire = 0f;
    }

    private void DestroyShield()
    {
        timeSinceShieldDestroyed = 0f;
        currentShield.onDestroy -= DestroyShield;
        destroyShieldPlayer.PlayRandomAudio();
        Destroy(currentShield.gameObject);
    }

    private void Update()
    {
        timeSinceWeaponFire += Time.deltaTime;
        timeSinceShieldDestroyed += Time.deltaTime;
    }
}
