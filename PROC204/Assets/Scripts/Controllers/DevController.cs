using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevController : MonoBehaviour
{

    Mover mover;
    Melee melee;

    void Awake()
    {
        mover = GetComponent<Mover>();
        melee = GetComponent<Melee>();
    }    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) mover.Jump();
    }

    private void FixedUpdate()
    {
        mover.Move(Input.GetAxisRaw("Horizontal"));

        if (Input.GetMouseButtonDown(0)) melee.Swing();
    }
}
