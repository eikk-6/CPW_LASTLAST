using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterHitRespawn : MonoBehaviour
{
    private bool isInvincible = false;
    private Transform respawnPoint;
    private NavMeshAgent agent;

    private void Start()
    {
        GameObject respawnObj = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnObj != null)
        {
            respawnPoint = respawnObj.transform;
        }
        else
        {
            Debug.LogError("Respawn 태그 오브젝트가 필요합니다.");
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogWarning("NavMeshAgent 컴포넌트가 없습니다.");
        }
    }

    public void HandleHit()
    {
        if (isInvincible) return;

        if (respawnPoint != null)
        {
            Debug.Log("몬스터 전체 리스폰 처리 중");

            if (agent != null)
            {
                agent.enabled = false; // NavMeshAgent 비활성화
            }

            transform.position = respawnPoint.position;

            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Debug.Log("무적 시작");

        yield return new WaitForSeconds(2f);

        if (agent != null)
        {
            agent.enabled = true; // NavMeshAgent 다시 활성화
        }

        isInvincible = false;
        Debug.Log("무적 해제");
    }
}
