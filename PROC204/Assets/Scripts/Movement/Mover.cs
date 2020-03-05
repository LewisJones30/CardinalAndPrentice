using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] Transform cardinalBody;

    Coroutine turning;

    Rigidbody rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(float input)
    {
        float moveForce = input * moveSpeed;

        rb.AddForce(new Vector3(moveForce, 0f, 0f), ForceMode.Acceleration);

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        float speed = rb.velocity.x;
        float dir = Mathf.Sign(speed);

        if (turning == null) turning = StartCoroutine(Turn());

        animator.SetFloat("forwardSpeed", Mathf.Abs(speed));
    }

    IEnumerator Turn()
    {
        Vector3 eulerRot = cardinalBody.rotation.eulerAngles;
        Quaternion targetRot = Quaternion.Euler(eulerRot + new Vector3(0f, 180f, 0f));

        while(true)
        {
            Quaternion newRot = Quaternion.Lerp(cardinalBody.rotation, targetRot, 0.1f);

            if (newRot == targetRot) break;

            cardinalBody.rotation = newRot;

            yield return null;
        }

        turning = null;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}
