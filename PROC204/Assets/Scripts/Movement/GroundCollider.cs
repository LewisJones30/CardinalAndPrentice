using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    public delegate void OnChangeGroundState(bool isGrounded);
    public event OnChangeGroundState onChangeGroundState;

    private void OnTriggerEnter(Collider other)
    {
        IsGrounded = true;
        onChangeGroundState?.Invoke(IsGrounded);
    }

    private void OnTriggerExit(Collider other)
    {
        IsGrounded = false;
        onChangeGroundState?.Invoke(IsGrounded);
    }
}
