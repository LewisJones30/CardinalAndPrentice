using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] Transform shootPosition;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float reloadTime = 0.4f;
    [SerializeField] float inaccuracy = 0f;

    [Header("Burst Properties")]
    [SerializeField] bool isBurst;
    [SerializeField] int clipSize = 3;
    [SerializeField] float clipReloadTime = 3f;

    [Header("Sound FX")]
    [SerializeField] RandomAudioPlayer attackPlayer;

    //STATES

    int currentClip;
    Vector3 targetPos;

    //PROPERTIES

    public override float AttackRate => reloadTime;

    private void Start()
    {
        currentClip = clipSize; //full reload at start
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    //Called by animation event from attack
    public void Shoot()
    {
        Vector3 launchPos = new Vector3(shootPosition.position.x, shootPosition.position.y, 0f);
        Projectile projectile = Instantiate(projectilePrefab, launchPos, Quaternion.identity);

        Vector3 targetDir = targetPos - launchPos;

        float randomAccuracy = Random.Range(-inaccuracy, inaccuracy);

        //Slight aim modification depending on inaccuracy of weapon
        Vector2 aim = new Vector2(targetDir.x + randomAccuracy, targetDir.y  + randomAccuracy); 

        projectile.SetDirection(aim.normalized);        
    }

    //Use weapon begins shoot animation
    public override void UseWeapon()
    {        
        if (!isReady) return;

        isReady = false;
        animator.SetTrigger("attackTrigger");
        if (attackPlayer != null) attackPlayer.PlayRandomAudio();

        float thisReload = reloadTime;
        if (isBurst) 
        {
            currentClip--;

            if (currentClip < 1) //reload longer than normal when reloading clip
            {
                thisReload = clipReloadTime;
                currentClip = clipSize;
            }
        }

        Invoke(nameof(WeaponReady), thisReload);
    }
}
