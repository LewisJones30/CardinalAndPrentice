using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    //Variables declaration
    PrenticeAttack prenticeAttack;
    [SerializeField] int additionalDamagePerStack = 1;
    int stackCount = 0;
    [SerializeField] Text comboText;
    Health playerHealth;
    [SerializeField] HealthUI healthUI;
    void Start()
    {
        prenticeAttack = GetComponentInChildren<PrenticeAttack>();
        playerHealth = GetComponent<Health>();
    }

    public void increaseDamage()
    {
        stackCount = stackCount + 1; //Track how many times the player has comboed.

        //Increase the damage of all projectiles by the additional damage per stack variable. 
        prenticeAttack.DamageIncrease += additionalDamagePerStack;

        Debug.Log("Bonus projectle damage:" + prenticeAttack.DamageIncrease);
        comboText.text = "Combo: " + stackCount;

        // When combo is 5 add 2 lives to Cardinal
        if (stackCount == 5)
        {
            playerHealth.health = playerHealth.health + 2;
            healthUI.AddOneHeart();
        }
    }
    public void decreaseDamage()
    {
        stackCount = 0; //Immediately reset the stack count to zero
        prenticeAttack.DamageIncrease = 0;

        comboText.text = "Combo: " + stackCount;
    }
}
