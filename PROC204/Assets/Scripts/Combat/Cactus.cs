using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] float damageRate = 1f;
    [SerializeField] int damage = 1;
    [SerializeField] float lifeTime = 30f;
    [SerializeField] RandomAudioPlayer despawnPlayer;

    //STATES

    //used to limit number of cacti
    public static int cactiCount = 0;

    //Keeps track of entities touching the cactus
    //Applies damage every set interval
    Dictionary<CombatTarget, float> targetsInContact = new Dictionary<CombatTarget, float>();

    //CACHE REFERENCES
    
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cactiCount++;
        StartCoroutine(Lifetime());
    }

    //Add new target in contact with cactus
    private void OnTriggerEnter(Collider other)
    {
        CombatTarget target = other.gameObject.GetComponent<CombatTarget>();
        if (target == null) return;

        if (!targetsInContact.ContainsKey(target))
        {
            targetsInContact[target] = damageRate; //Damage next update
        }
    }

    //Remove target in contact with cactus
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

        //Prevents animation trigger being called more than once this frame
        bool hasAttacked = false; 

        foreach (var target in targets)
        {
            if (target.IsDead) //Validate target
            {
                targetsInContact.Remove(target);
                continue;
            }

            targetsInContact[target] += Time.deltaTime;
            if (targetsInContact[target] >= damageRate) //Damage enemy again if contact is sustained for set damage rate
            {
                target.TakeDamage(damage, transform.position);
                targetsInContact[target] = 0f;
                hasAttacked = true;
            }
        }
        if (animator == null) return;
        if (hasAttacked) animator.SetTrigger("AttackTrigger");
    }

    //Cacti dies after limited life time is over
    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        cactiCount--;
        despawnPlayer.PlayRandomAudio();
        animator.SetTrigger("DeathTrigger");
        Destroy(gameObject, 1f); //Delay destroy for animation
    }
}
