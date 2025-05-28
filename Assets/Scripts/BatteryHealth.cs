using UnityEngine;
using System.Collections;

public class BatteryHealth : MonoBehaviour, ITargetable
{
    public int maxHealth = 10;
    private int currentHealth;

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
        Debug.Log($"배터리 피해: {currentHealth}");

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
        Debug.Log("배터리 파괴됨! 게임 오버 처리 필요");

        // 배터리 파괴 후 게임 종료 처리나 비활성화 가능
        gameObject.SetActive(false);

        // TODO: 게임 종료 처리 로직 호출
        // ex: GameManager.Instance.GameOver();
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
