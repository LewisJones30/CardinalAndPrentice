using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevController : MonoBehaviour
{

    Mover mover;
    Melee melee;
    Health health;

    void Awake()
    {
        mover = GetComponent<Mover>();
        melee = GetComponent<Melee>();
        health = GetComponent<Health>();
    }    

    private void Update()
    {
        if (health.IsDead) return;

        if (Input.GetKeyDown(KeyCode.Space)) mover.Jump();
    }

    private void FixedUpdate()
    {
        if (health.IsDead) return;

        mover.Move(Input.GetAxisRaw("Horizontal"), 1f);

        if (Input.GetMouseButtonDown(0)) melee.Swing();
    }
}
