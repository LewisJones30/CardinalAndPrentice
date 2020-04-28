using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusProjectile : PrenticeProjectile
{
    [SerializeField] GameObject cactusPrefab;
    [SerializeField] int maxCacti = 3;

    //STATES

    //Prevents projectile from spawning more than one cactus
    bool spawnedCactus = false;

    private void Awake()
    {
        //No homing because player may want projectile to hit ground
        //so that projectile spawns a cactus
        isHoming = false;
    }

    //Damage hit enemy otherwise spawn a cactus
    protected override void OnTriggerEnter(Collider other)
    {
        CombatTarget combatTarget = other.gameObject.GetComponentInParent<CombatTarget>();
        if (combatTarget != null) combatTarget.TakeDamage(damage, projectileColour, transform.position);
        else SpawnCactus(other.ClosestPointOnBounds(transform.position));

        GameObject instance = Instantiate(projectileHitFX, other.ClosestPointOnBounds(transform.position), Quaternion.identity); //Hit VFX
        Destroy(instance, 10f);

        Destroy(gameObject);
    }

    //Spawn cactus if limit not reached
    private void SpawnCactus(Vector3 spawnPos)
    {
        if (spawnedCactus || Cactus.cactiCount >= maxCacti) return;
        spawnedCactus = true;
        Instantiate(cactusPrefab, spawnPos, Quaternion.identity);
    }
}
