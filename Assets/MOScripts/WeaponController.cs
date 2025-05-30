using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform playerTransform;
    public string ownerTag = "Player"; // "Player" or "Enemy"

    private void OnTriggerEnter(Collider other)
    {
        // 상대 무기와 충돌 시
        if (ownerTag == "Player" && other.CompareTag("EnemyWeapon"))
        {
            PushBackIfNotSelf(other);
        }
        else if (ownerTag == "Enemy" && other.CompareTag("Weapon"))
        {
            PushBackIfNotSelf(other);
        }

        // 상대 방패와 충돌 시
        if (ownerTag == "Player" && other.CompareTag("EnemyShield"))
        {
            PushBackIfNotSelf(other);
        }
        else if (ownerTag == "Enemy" && other.CompareTag("Shield"))
        {
            PushBackIfNotSelf(other);
        }
    }

    private void PushBackIfNotSelf(Collider other)
    {
        if (other.transform.root == playerTransform.root)
        {
            // 내 무기/방패랑 충돌했으므로 무시
            return;
        }

        PushBack(other.transform.position);
    }

    public void PushBack(Vector3 hitPoint)
    {
        if (playerTransform == null) return;

        Vector3 direction = (playerTransform.position - hitPoint).normalized;
        direction.y = 0f;

        Vector3 pushPosition = playerTransform.position + direction * 1.5f;
        playerTransform.position = pushPosition;

        Debug.Log($"{ownerTag} pushed back by enemy!");
    }
}
