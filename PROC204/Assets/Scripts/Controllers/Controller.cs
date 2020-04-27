using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected bool isFrozen = false;
    protected bool blockInput = false;

    //Can be set manually set to block movement fo a time 
    //during a cutscene for example
    public bool BlockInput
    {
        get => blockInput;
        set
        {
            blockInput = value;
            FreezeInput();
        }
    }

    bool isStunned = false;
    bool isDead = false;

    //Called when message broadcast from combat target
    //notifying that the character is stunned
    void StunUpdate(bool isStunned)
    {
        this.isStunned = isStunned;
        FreezeInput();
    }

    //Called when message broadcast from health
    //notifying that the character is dead
    void DeadUpdate(bool isDead)
    {
        this.isDead = isDead;
        FreezeInput();
    }

    //Character cannot move when input blocked manually, dead or stunned
    void FreezeInput()
    {
        isFrozen = blockInput || isDead || (isStunned && !isDead);
    }
}
