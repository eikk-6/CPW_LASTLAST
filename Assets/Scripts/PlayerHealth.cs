using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, ITargetable
{
    public int maxHealth = 1;
    private int currentHealth;
    public Transform respawnPoint;

    public float invulnerabilityDuration = 3f;  // ���� �ð� (��)
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
        Debug.Log($"�÷��̾� ����: {currentHealth}");

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
        Debug.Log("�÷��̾� ��� �� ������");
        currentHealth = maxHealth;

        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("������ ��ġ�� �������� �ʾҽ��ϴ�.");
        }

        StartCoroutine(InvulnerabilityTimer()); // ������ �Ŀ��� ��� ����
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
