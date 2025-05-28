using UnityEngine;
using System.Collections;

public class BatteryHealth : MonoBehaviour, ITargetable
{
    public int maxHealth = 10;
    private int currentHealth;

    public float invulnerabilityDuration = 1f;  // ���� �ð� (��)
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
        Debug.Log($"���͸� ����: {currentHealth}");

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
        Debug.Log("���͸� �ı���! ���� ���� ó�� �ʿ�");

        // ���͸� �ı� �� ���� ���� ó���� ��Ȱ��ȭ ����
        gameObject.SetActive(false);

        // TODO: ���� ���� ó�� ���� ȣ��
        // ex: GameManager.Instance.GameOver();
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
