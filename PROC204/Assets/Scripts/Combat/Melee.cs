using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Melee : MonoBehaviour
{
    [SerializeField] int damage = 20;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackHeight = 0.9f;
    [SerializeField] LayerMask hitLayers;

    Vector3 hitBox;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        hitBox = new Vector3(attackRange / 2, attackHeight / 2, 1f / 2);
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            attackPoint.position = new Vector3(attackRange / 2, attackPoint.position.y, 0f);
        }        
    }

    public void Swing()
    {
        animator.SetTrigger("attackTrigger");        
    }

    void Hit()
    {
        Collider[] colliders = Physics.OverlapBox(attackPoint.position, hitBox, Quaternion.identity, hitLayers);

        foreach (Collider collider in colliders)
        {
            Health health = collider.gameObject.GetComponent<Health>();

            if (health != null) health.DealDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, attackHeight, 1f));
    }
}
