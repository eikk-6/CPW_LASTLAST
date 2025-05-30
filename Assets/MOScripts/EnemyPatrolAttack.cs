using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAttack : MonoBehaviour
{
    public Transform player;
    public Transform battery;
    public Transform[] patrolPoints;

    public SwordAttack leftSword;
    public SwordAttack rightSword;

    private NavMeshAgent agent;
    private float distanceToTarget;
    private int patrolIndex;
    private Transform currentTarget;

    public float attackRange = 2f;
    public float chaseRange = 10f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolIndex = 0;
        GoToNextPatrolPoint();
    }

    private void Update()
    {
        if (player == null || battery == null)
        {
            Patrol();
            return;
        }

        float distToPlayer = Vector3.Distance(transform.position, player.position);
        float distToBattery = Vector3.Distance(transform.position, battery.position);

        // 가까운 대상 선택
        if (distToPlayer < distToBattery)
        {
            currentTarget = player;
            distanceToTarget = distToPlayer;
        }
        else
        {
            currentTarget = battery;
            distanceToTarget = distToBattery;
        }

        if (distanceToTarget <= attackRange)
        {
            agent.isStopped = true;
            Debug.Log("공격!! 대상: " + currentTarget.name);

            // 👇 두 개의 검 휘두르기
            if (leftSword != null) leftSword.Attack();
            if (rightSword != null) rightSword.Attack();
        }
        else if (distanceToTarget <= chaseRange)
        {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
            Debug.Log("추적 중... 대상: " + currentTarget.name);
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agent.isStopped = false;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            GoToNextPatrolPoint();
        }

        Debug.Log("순찰 중...");
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[patrolIndex].position);
    }
}
