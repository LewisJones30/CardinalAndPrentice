using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    //Variables declaration
    [SerializeField] float reloadReductionPerStack = 0.05f;
    [SerializeField] Text comboText;

    //CACHE REFERENCES

    Fighter fighter;
    PrenticeAttack prenticeAttack;
    Health playerHealth;

    //STATES

    //Combo counter
    int stackCount = 0;

    private void Awake()
    {
        fighter = GetComponent<Fighter>(); 
        prenticeAttack = GetComponentInChildren<PrenticeAttack>();
        playerHealth = GetComponent<Health>();
    }    

    void Start()
    {
        //build combo when damage dealt
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

        // When combo is 5 add 2 lives to Cardinal
        if (stackCount == 5)
        {
            playerHealth.ChangeHealth(2);
        }

        comboText.text = "Combo: " + stackCount;
    }
    public void BreakCombo()
    {
        stackCount = 0; //Immediately reset the stack count to zero
        prenticeAttack.ReloadReduction = 0;

        comboText.text = "Combo: " + stackCount;
    }
}
