using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    [SerializeField] Transform groundRaycastPoint;

    public bool IsGrounded { get; private set; }
    int layerMask;

    private void Start()
    {
        layerMask = 1 << 9;
        layerMask = layerMask << 11;
        layerMask = ~layerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsGrounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        IsGrounded = true;
    }

    public Vector3 CalculateGroundDirection(Vector3 characterDir)
    {
        if (!IsGrounded) return Vector3.right;

        bool isHit = Physics.Raycast(groundRaycastPoint.position, Vector3.down, out RaycastHit hit, 1000f, layerMask);

        if (!isHit) return Vector3.right;

        return Vector3.Cross(characterDir, hit.normal);
    }
}
