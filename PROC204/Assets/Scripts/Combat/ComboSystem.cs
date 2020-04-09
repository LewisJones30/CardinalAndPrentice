using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    //Variables declaration
    PrenticeAttack prenticeAttack;
    [SerializeField] float reloadReductionPerStack = 0.05f;
    int stackCount = 0;
    [SerializeField] Text comboText;
    Health playerHealth;
    [SerializeField] HealthUI healthUI;

    Fighter fighter;

    private void Awake()
    {
        fighter = GetComponent<Fighter>();
    }    

    void Start()
    {
        prenticeAttack = GetComponentInChildren<PrenticeAttack>();
        playerHealth = GetComponent<Health>();

        fighter.MeleeWeapon.onDealDamage += BuildCombo;
    }

    private void OnDisable()
    {
        fighter.MeleeWeapon.onDealDamage -= BuildCombo;
    }

    public void BuildCombo()
    {
        stackCount = stackCount + 1; //Track how many times the player has comboed.

        //Increase the damage of all projectiles by the additional damage per stack variable. 
        prenticeAttack.ReloadReduction += reloadReductionPerStack;

        Debug.Log("Projectile reload reduction: -" + prenticeAttack.ReloadReduction);
        comboText.text = "Combo: " + stackCount;

        // When combo is 5 add 2 lives to Cardinal
        if (stackCount == 5)
        {
            playerHealth.health = playerHealth.health + 2;
            healthUI.AddOneHeart();
        }
    }
    public void BreakCombo()
    {
        stackCount = 0; //Immediately reset the stack count to zero
        prenticeAttack.ReloadReduction = 0;

        comboText.text = "Combo: " + stackCount;
    }
}
