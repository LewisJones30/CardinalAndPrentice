using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected bool isFrozen = false;
    protected bool blockInput = false;

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

    void StunUpdate(bool isStunned)
    {
        this.isStunned = isStunned;
        FreezeInput();
    }

    void DeadUpdate(bool isDead)
    {
        this.isDead = isDead;
        FreezeInput();
    }

    void FreezeInput()
    {
        isFrozen = blockInput || isDead || (isStunned && !isDead);
    }
}
