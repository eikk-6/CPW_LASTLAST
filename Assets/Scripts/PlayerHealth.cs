using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, ITargetable
{
    public int maxHealth = 5;
    private int currentHealth;
    public Transform respawnPoint;

    public float invulnerabilityDuration = 1f;  // 무적 시간 (초)
    private bool isInvulnerable = false;

    public Transform GetTransform() => transform;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return;

        currentHealth -= amount;
        Debug.Log($"플레이어 피해: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityTimer());
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망 → 리스폰");
        currentHealth = maxHealth;

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("리스폰 위치가 지정되지 않았습니다.");
        }

        StartCoroutine(InvulnerabilityTimer()); // 리스폰 후에도 잠시 무적
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
