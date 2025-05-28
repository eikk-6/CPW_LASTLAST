using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        // 상대 무기와 충돌 시
        if (other.CompareTag("EnemyWeapon"))
        {
            PushBack(other.transform.position);
        }
    }

    public void PushBack(Vector3 hitPoint) //무기 사용 플레이어 뒤로 밀려남
    {
        if (playerTransform == null) return;

        Vector3 direction = (playerTransform.position - hitPoint).normalized;
        Vector3 pushPosition = playerTransform.position + direction * 0.5f;

        playerTransform.position = pushPosition;

        Debug.Log("Player pushed back by enemy weapon!");
    }
}
