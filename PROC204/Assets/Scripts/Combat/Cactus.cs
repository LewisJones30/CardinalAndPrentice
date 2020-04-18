using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] float damageRate = 1f;
    [SerializeField] int damage = 1;
    [SerializeField] float lifeTime = 30f;

    public static int cactiCount = 0;

    Dictionary<CombatTarget, float> targetsInContact = new Dictionary<CombatTarget, float>();

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cactiCount++;
        StartCoroutine(Lifetime());
    }

    private void OnTriggerEnter(Collider other)
    {
        CombatTarget target = other.gameObject.GetComponent<CombatTarget>();
        if (target == null) return;

        if (!targetsInContact.ContainsKey(target))
        {
            targetsInContact[target] = damageRate;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CombatTarget target = other.gameObject.GetComponent<CombatTarget>();
        if (target == null) return;

        if (targetsInContact.ContainsKey(target))
        {
            targetsInContact.Remove(target);
        }
    }

    private void Update()
    {
        CombatTarget[] targets = new CombatTarget[targetsInContact.Count];
        targetsInContact.Keys.CopyTo(targets, 0);

        bool hasAttacked = false;

        foreach (var target in targets)
        {
            if (target.IsDead)
            {
                targetsInContact.Remove(target);
                continue;
            }

            targetsInContact[target] += Time.deltaTime;
            if (targetsInContact[target] >= damageRate)
            {
                target.TakeDamage(1, transform.position);
                targetsInContact[target] = 0f;
                hasAttacked = true;
            }
        }

        if (hasAttacked) animator.SetTrigger("AttackTrigger");
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        cactiCount--;
        animator.SetTrigger("DeathTrigger");
        Destroy(gameObject, 1f);
    }
}
