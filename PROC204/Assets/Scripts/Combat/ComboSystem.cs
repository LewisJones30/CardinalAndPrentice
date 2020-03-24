﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    //Variables declaration
    RangedWeapon projectileScript;
    int originalDamage;
    [SerializeField] int additionalDamagePerStack = 1;
    int stackCount = 0;
    void Start()
    {
        GameObject prentice = GameObject.Find("Prentice");
        projectileScript = prentice.GetComponent<RangedWeapon>();
        originalDamage = projectileScript.redProjectile.damage; //All default projectile damage is the same, so only needs to be obtained from one source.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseDamage()
    {
        stackCount = stackCount + 1; //Track how many times the player has comboed.
        //Increase the damage of each of the projectiles by the additional damage per stack variable. 
        projectileScript.redProjectile.damage = projectileScript.redProjectile.damage + additionalDamagePerStack;
        projectileScript.yellowProjectile.damage = projectileScript.yellowProjectile.damage + additionalDamagePerStack;
        projectileScript.greenProjectile.damage = projectileScript.greenProjectile.damage + additionalDamagePerStack;
        projectileScript.blueProjectile.damage = projectileScript.blueProjectile.damage + additionalDamagePerStack;
        Debug.Log("Projectile damage:" + projectileScript.redProjectile.damage);
    }
    public void decreaseDamage()
    {
        stackCount = 0; //Immediately reset the stack count to zero
        projectileScript.redProjectile.damage = originalDamage;
        projectileScript.yellowProjectile.damage = originalDamage;
        projectileScript.greenProjectile.damage = originalDamage;
        projectileScript.blueProjectile.damage = originalDamage;
    }
}
