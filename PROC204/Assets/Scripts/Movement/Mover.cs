using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 20f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(float input)
    {
        float moveForce = input * moveSpeed;

        rb.AddForce(new Vector3(moveForce, 0f, 0f), ForceMode.Acceleration);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}
