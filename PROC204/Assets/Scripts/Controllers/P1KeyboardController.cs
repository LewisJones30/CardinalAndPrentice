using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1KeyboardController : MonoBehaviour
{
    Mover mover;

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        mover.Move(Input.GetAxis("Horizontal"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) mover.Jump();
    }
}
