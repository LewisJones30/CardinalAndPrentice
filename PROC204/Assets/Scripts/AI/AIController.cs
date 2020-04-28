using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    [SerializeField] float patrolSpeedFraction = 0.6f;
    [SerializeField] float pursueSpeedFraction = 0.8f;
    [SerializeField] float minSpeedFraction = 0.3f;
    [SerializeField] float traverseRefreshRate = 1f;
    [SerializeField] float reactionTime = 0.5f;
    [SerializeField] float spotTargetDistance = 5f;
    [SerializeField] float loseTargetDistance = 10f;
    [SerializeField] float maxWaitTime = 4f;
    [Range(0f, 1f)]
    [SerializeField] float jumpChance = 0.5f;
    [Range(0f, 0.95f)]
    [SerializeField] float randomness = 0.2f;

    //STATES

    //Target to pursue
    Health currentTarget;

    //Changes when patroling or pursuing
    float speedFraction = 1f;

    //Determines how far the enemy can shoot at a target
    float preferredShootingRange;

    //Enemy tries to remain at this height when flying
    float preferredHeight;

    //Desired vertical movement used when flying
    float yMovement = 0f;

    //CACHE REFERENCES

    Mover mover;
    Fighter fighter;
    Health health;
    CharacterController charController;

    bool isStationary = false;
    float moveDir = 1f;
    bool canTraverse = true;
    float waitTime = Mathf.Infinity;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        //Losing sight of target must be equal to / greater than the spotting distance
        if (loseTargetDistance < spotTargetDistance) loseTargetDistance = spotTargetDistance;

        //Middle of spotting and losing distance
        preferredShootingRange = (spotTargetDistance + loseTargetDistance) / 2;

        //Gets character start y position
        preferredHeight = mover.Position.y;

        StartCoroutine(AIBrain());
    }

    //Used to multiply against parameters to increase randomness factor
    private float GetRandomMultiplier()
    {
        return UnityEngine.Random.Range(1 - randomness, 1 + randomness);
    }

    //Called less frequently than Update() to increase performance
    //Tries to mimic varied human response time
    IEnumerator AIBrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(reactionTime * GetRandomMultiplier());
            if (isFrozen) continue;

            ValidateTarget();
            FindTarget();
            Wait();
            Movement();
            Attack();
        }
    }

    //AI waits when target has just been lost
    //Stops waiting when wait time meets a set value
    private void Wait()
    {
        if (currentTarget == null && waitTime < maxWaitTime) isStationary = true;
        else isStationary = false;
    }
    
    //AI constantly moves unless waiting or target in range
    private void Movement()
    {
        FaceTarget();
        
        if (currentTarget == null) speedFraction = patrolSpeedFraction; //Walk is default speed
        else speedFraction = pursueSpeedFraction;

        // AI either jumps, rolls or turns when moving,  moving below a certain threshold
        // and touching obstacles to either the left or right to prevent them from getting stuck
        if (Mathf.Abs(mover.GetVelocity().x) < (minSpeedFraction * mover.MaxSpeed) && 
            mover.IsStuck && !isStationary) TraverseObstacle();
    }

    //Turns to face target if target present
    private void FaceTarget()
    {
        if (currentTarget == null) return;

        float targetDirection = Mathf.Sign(currentTarget.transform.position.x - transform.position.x);
        if (targetDirection != moveDir) Turn();
    }

    //Deals with blocking obstacles or characters
    //Either jumps, rolls or turn depending on the circumstance
    private void TraverseObstacle()
    {
        if (!canTraverse) return;

        //Limits traverse rate to prevent spamming
        canTraverse = false;
        Invoke(nameof(RefreshTraverse), traverseRefreshRate);

        //Chance of jumping since this can be done to traverse most obstacles
        if (UnityEngine.Random.value <= jumpChance * GetRandomMultiplier())
        {
            mover.Jump();
            return;
        }

        //Checks if blocked by other AI
        int enemyLayerMask = LayerMask.GetMask("Enemy");
        bool isHit = Physics.Raycast(mover.Position, Vector3.right * mover.Direction, charController.radius * 2f, enemyLayerMask);

        if (isHit) mover.ForwardRoll(); //Roll through AI characters
        else if (currentTarget == null) Turn(); //Can only turn when NOT pursuing target
        else mover.Jump(); //Jump by default
    }

    //Looks for target within range if no current target
    private void FindTarget()
    {
        if (currentTarget != null) return;

        Collider[] targets = Physics.OverlapSphere(mover.Position, spotTargetDistance, fighter.TargetLayerMask);

        if (targets.Length < 1) return; //No targets in range

        foreach (var target in targets)
        {
            Health health = target.gameObject.GetComponentInParent<Health>();
            if (health != null && !health.IsDead) //Validate target
            {
                currentTarget = health;
            }
        }
    }

    //Only attack if target present
    void Attack()
    {
        if (currentTarget == null) return;

        if (fighter.IsMelee)
        {
            isStationary = MeleeAttack(); //Remain stationary if can hit with melee weapon
        }
        else
        {
            isStationary = RangeAttack(); //Remain stationary if can shoot target
        }
    }

    //Called when holding melee weapon
    // Returns true when enemy is in range to hit the player
    private bool MeleeAttack()
    {
        if (!fighter.MeleeWeapon.CheckTargetInMeleeRange()) return false;

        fighter.Attack();
        return true;
    }

    //Called when holding ranged weapon
    // Returns true when player is in line of sight of enemy
    private bool RangeAttack()
    {
        if (currentTarget == null) return false;

        Vector3 targetPos = currentTarget.GetComponent<Mover>().Position;
        Vector3 targetDir = targetPos - mover.Position;

        //Projectile can hit these layers
        int shootLayerMask = LayerMask.GetMask("Player", "Default");
        bool isHit = Physics.Raycast(mover.Position, targetDir, out RaycastHit hit, preferredShootingRange, shootLayerMask);

        if (!isHit) return false; //Shooting blocked?

        Health health = hit.collider.gameObject.GetComponentInParent<Health>();
        if (health == null) return false; //Validate target in sight

        fighter.RangeWeapon.SetTarget(targetPos);
        fighter.Attack();

        return true;
    }

    //AI moves each frame based on direction and speed fraction
    private void Update()
    {
        if (isFrozen) return;

        waitTime += Time.deltaTime;

        if (mover.Position.y < preferredHeight) yMovement = 1f; //Fly up when lower than preferred height
        else yMovement = 0f; //Otherwise allow gravity to pull down

        if (!isStationary) mover.Move(new Vector3(moveDir, yMovement, 0f), speedFraction);
        else mover.Move(new Vector3(0f, yMovement, 0f), speedFraction);
    }

    //Check if set target is still in range or dead
    private void ValidateTarget()
    {
        if (currentTarget == null) return;
        
        float targetDistance = Vector3.Distance(transform.position, currentTarget.transform.position);

        if (targetDistance > loseTargetDistance || currentTarget.IsDead)
        {
            currentTarget = null; //Lose target when invalid
            waitTime = 0f;
        }
    }

    //Flip character model and change move direction
    private void Turn()
    {
        moveDir *= -1;
        mover.Turn(moveDir);
    }

    //Called when refresh timer complete
    private void RefreshTraverse()
    {
        canTraverse = true;
    }

    //Visualize spotting and losing distance
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spotTargetDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loseTargetDistance);
    }
}
