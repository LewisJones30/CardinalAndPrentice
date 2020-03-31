using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    //Variables declaration
    RangedWeapon projectileScript;
    float originalReload;
    [SerializeField] int additionalDamagePerStack = 1;
    int stackCount = 0;
    [SerializeField] Text comboText;
    Health playerHealth;
    [SerializeField] HealthUI healthUI;
    void Start()
    {
        GameObject prentice = GameObject.Find("Prentice");
        projectileScript = prentice.GetComponent<RangedWeapon>();
        originalReload = projectileScript.redProjectile.reloadTime; //All default projectile damage is the same, so only needs to be obtained from one source.
        playerHealth = this.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseDamage()
    {
        stackCount = stackCount + 1; //Track how many times the player has comboed.
        //Increase the damage of each of the projectiles by the additional damage per stack variable. 
        projectileScript.redProjectile.reloadTime = projectileScript.redProjectile.reloadTime - 0.05f;
        projectileScript.yellowProjectile.reloadTime = projectileScript.yellowProjectile.reloadTime - 0.05f;
        projectileScript.greenProjectile.reloadTime = projectileScript.greenProjectile.reloadTime - 0.05f;
        projectileScript.blueProjectile.reloadTime = projectileScript.blueProjectile.reloadTime - 0.05f;
        Debug.Log("Projectile damage:" + projectileScript.redProjectile.reloadTime);
        comboText.text = "Combo: " + stackCount;
        if (stackCount == 5)
        {
            playerHealth.health = playerHealth.health + 2;
            healthUI.AddOneHeart();
        }
    }
    public void decreaseDamage()
    {
        stackCount = 0; //Immediately reset the stack count to zero
        projectileScript.redProjectile.reloadTime = originalReload;
        projectileScript.yellowProjectile.reloadTime = originalReload;
        projectileScript.greenProjectile.reloadTime = originalReload;
        projectileScript.blueProjectile.reloadTime = originalReload;
        comboText.text = "Combo: " + stackCount;
    }
}
