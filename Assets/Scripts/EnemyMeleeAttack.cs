using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemyMeleeAttack : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private CapsuleCollider monsterCollider;

    private bool isAttacking;
    private bool isDeath;

    private float distance;
    private float timer;
    private float attackCooldown = 2f;

    public float attackRange = 2f;
    public int damage = 10;

    private void OnEnable()
    {
        if (monsterCollider != null)
            monsterCollider.enabled = true;

        if (navMeshAgent != null)
        {
            navMeshAgent.speed = 2f;
            navMeshAgent.isStopped = false;
        }

        isAttacking = false;
        isDeath = false;
        timer = 0f;
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monsterCollider = GetComponent<CapsuleCollider>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (navMeshAgent != null)
            navMeshAgent.enabled = true;
    }

    private void Update()
    {
        if (isDeath || player == null) return;

        distance = Vector3.Distance(transform.position, player.position);
        navMeshAgent.SetDestination(player.position);

        if (distance <= attackRange)
        {
            Attack();
        }
        else
        {
            animator.SetTrigger("Run");
            navMeshAgent.isStopped = false;
        }
    }

    private void Attack()
    {
        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            timer = 0f;
            isAttacking = true;

            navMeshAgent.isStopped = true;
            animator.SetTrigger("Attack");

            var health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDeath) return;

        isDeath = true;
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;

        animator.SetTrigger("Death");

        if (monsterCollider != null)
            monsterCollider.enabled = false;

        StartCoroutine(DeactivateAfterDeath());
    }

    private IEnumerator DeactivateAfterDeath()
    {
        yield return new WaitForSeconds(2f); // 애니메이션 길이
        gameObject.SetActive(false); // 오브젝트 풀로 되돌아감
    }
}
