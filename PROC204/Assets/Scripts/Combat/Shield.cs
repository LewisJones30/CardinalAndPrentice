using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float bounciness = 70f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody victim = collision.gameObject.GetComponent<Rigidbody>();

        if (victim == null) return;
        victim.AddForce(transform.right * bounciness, ForceMode.Impulse);
    }
}
