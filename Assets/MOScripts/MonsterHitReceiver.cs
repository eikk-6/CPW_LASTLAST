using UnityEngine;

public class MonsterHitReceiver : MonoBehaviour
{
    private MonsterHitRespawn respawnScript;

    private void Start()
    {
        respawnScript = GetComponentInParent<MonsterHitRespawn>();
        if (respawnScript == null)
        {
            Debug.LogError("부모에 MonsterHitRespawn 스크립트가 없습니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            respawnScript?.HandleHit();
        }
    }
}
