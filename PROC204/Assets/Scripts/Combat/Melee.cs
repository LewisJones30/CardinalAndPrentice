using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Swing()
    {
        animator.SetTrigger("attackTrigger");
    }

    public void Hit()
    {

    }
}
