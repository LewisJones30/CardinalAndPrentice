using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float acceleration = 60f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] Transform cardinalBody;

    bool isGrounded = true;

    Rigidbody rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(float input)
    {
        float moveForce = input * acceleration;

        rb.AddForce(new Vector3(moveForce, 0f, 0f), ForceMode.Acceleration);

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        float speed = rb.velocity.x;
        Turn(speed);

        animator.SetFloat("forwardSpeed", Mathf.Abs(speed));
    }

    private void Turn(float input)
    {
        if (Mathf.Abs(input) < 0.1f) return;

        if (input > 0)
        {
            cardinalBody.right = new Vector3(0, 0, -1);
        }
        else if (input < 0)
        {
            cardinalBody.right = new Vector3(0, 0, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        animator.SetBool("isGrounded", true);
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        animator.SetBool("isGrounded", false);
    }

    public void Jump()
    {
        if (!isGrounded) return;
        animator.SetTrigger("jumpTrigger");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}
