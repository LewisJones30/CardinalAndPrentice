using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float minBounce = 30f;
    [SerializeField] float maxBounce = 50f;

    public delegate void OnDestroy();
    public event OnDestroy onDestroy;

    //Applies knockback and stun to enemies that touch shield
    private void OnTriggerEnter(Collider other)
    {
        CharacterPhysics charPhysics = other.gameObject.GetComponent<CharacterPhysics>();

        if (charPhysics == null) return;

        Vector3 dir = new Vector3(transform.right.x, transform.right.y + UnityEngine.Random.value, 0f); //Apply horizontal and upward pushback force
        Vector3 knockBackForce = dir.normalized * Random.Range(minBounce, maxBounce); //Generate random magnitude
        charPhysics.KnockBack(knockBackForce, 3f, Time.time);

        onDestroy?.Invoke();
    }
}
