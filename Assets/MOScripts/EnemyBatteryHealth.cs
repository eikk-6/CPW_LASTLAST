using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBatteryHealth : MonoBehaviour
{
    public int batteryHp = 10;
    private bool isInvincible = false;
    private float invincibleTime = 3f;
    private float damageCooldown = 0.5f;
    private float lastDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        // Weapon에만 반응 (즉 플레이어 무기만 반응)
        if (other.CompareTag("Weapon"))
        {
            if (isInvincible) return;

            if (Time.time - lastDamageTime >= damageCooldown)
            {
                batteryHp -= 1;
                lastDamageTime = Time.time;
                Debug.Log("Enemy Battery hit! Current HP: " + batteryHp);

                if (batteryHp <= 0)
                {
                    SceneManager.LoadScene("EndingScene");
                }
                else
                {
                    StartCoroutine(StartInvincibility());
                }
            }
        }
    }

    private System.Collections.IEnumerator StartInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        Debug.Log("무적 해제");
    }
}
