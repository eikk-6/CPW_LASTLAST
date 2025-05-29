using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryHealth : MonoBehaviour
{
    public int batteryHp = 10;
    private bool isInvincible = false;
    private float invincibleTime = 3f;

    private float damageCooldown = 0.5f; // 데미지 입는 간격
    private float lastDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            if (isInvincible) 
            {
                Debug.Log("무적 상태 - 피격 무효");
                return;
            }

            // 데미지 입는 쿨타임 검사
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                batteryHp -= 1;
                lastDamageTime = Time.time;
                Debug.Log("Battery hit! Current HP: " + batteryHp);

                WeaponController weapon = other.GetComponent<WeaponController>();
                if (weapon != null)
                {
                    weapon.PushBack(transform.position);
                }

                if (batteryHp <= 0)
                {
                    SceneManager.LoadScene("CPWMainScene");
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
