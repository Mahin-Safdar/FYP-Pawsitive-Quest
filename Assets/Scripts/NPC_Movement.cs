using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Movement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;
    public float detectionRadius = 10.0f;
    public float attackDistance = 2.0f;
    public float patrolRange = 10.0f;
    public float idleTime = 3.0f;
    public float attackDamageDelay = 0.5f;  // Delay before applying damage

    private bool isAttacking = false;
    private Vector3 patrolPoint;
    private LifeManager playerLivesManager; // Correct reference to LiveManager
    private bool patrolPointSet = false; // Prevent NPC from standing still while patrolling

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();
        playerLivesManager = FindObjectOfType<LifeManager>();

        StartCoroutine(IdleBeforePatrol()); // Start in Idle and then patrol
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer <= attackDistance)
            {
                AttackPlayer(); // Transition to Attack
            }
            else
            {
                FollowPlayer(); // Transition to Follow Player
            }
        }
        else
        {
            if (!isAttacking)
            {
                Patrol(); // Transition to Patrol when player is out of range
            }
        }
    }

    // NPC Patrols
    void Patrol()
    {
        if (!patrolPointSet || (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending))
        {
            SetRandomPatrolPoint();
            patrolPointSet = true;
            agent.SetDestination(patrolPoint);
            animator.SetBool("isPatrolling", true);  // Enable Patrol animation
        }
    }

    // Set random patrol point
    void SetRandomPatrolPoint()
    {
        Vector3 randomPoint;
        if (RandomPoint(transform.position, patrolRange, out randomPoint))
        {
            patrolPoint = randomPoint;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    // NPC follows player
    void FollowPlayer()
    {
        if (!isAttacking)
        {
            agent.SetDestination(player.position);  // Follow player
            animator.SetBool("isPatrolling", false);  // Disable Patrol animation
        }
    }

    // NPC attacks player
    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            agent.SetDestination(transform.position);  // Stop moving
            animator.SetTrigger("Attack");  // Trigger attack animation
            animator.SetBool("isPatrolling", false);  // Disable Patrol animation

            StartCoroutine(ApplyDamageAfterDelay());  // Delay to apply damage
            StartCoroutine(AttackCooldown());  // Attack cooldown before returning to Patrol
        }
    }

    // Apply damage to the player after the delay (instead of Animation Event)
    IEnumerator ApplyDamageAfterDelay()
    {
        yield return new WaitForSeconds(attackDamageDelay);  // Wait before applying damage
        if (playerLivesManager != null)
        {
            playerLivesManager.LoseLife();  // Decrease player's life
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1.0f);  // Adjust cooldown as needed
        isAttacking = false;
        patrolPointSet = false;  // Reset patrol point for new patrol
        animator.ResetTrigger("Attack");
        animator.SetBool("isPatrolling", true);  // Return to patrol state after attacking
    }

    // NPC idles before patrolling
    IEnumerator IdleBeforePatrol()
    {
        animator.SetBool("isPatrolling", false);  // Idle animation
        yield return new WaitForSeconds(idleTime);

        SetRandomPatrolPoint();
        agent.SetDestination(patrolPoint);
        animator.SetBool("isPatrolling", true);  // Start patrol animation
    }
}
