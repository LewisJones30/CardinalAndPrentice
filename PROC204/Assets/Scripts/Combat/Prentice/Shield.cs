using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float bounciness = 70f;

    private void OnTriggerEnter(Collider other)
    {
        Fighter fighter = other.gameObject.GetComponent<Fighter>();

        if (fighter == null) return;

        fighter.Knockback(transform.right, 50f, 3f);
    }
}
