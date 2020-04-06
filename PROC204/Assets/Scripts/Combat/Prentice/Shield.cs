using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float bounciness = 70f;

    private void OnTriggerEnter(Collider other)
    {
        Mover mover = other.gameObject.GetComponent<Mover>();

        if (mover == null) return;
    }
}
