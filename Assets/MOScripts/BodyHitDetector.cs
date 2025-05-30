using UnityEngine;

public class BodyHitDetector : MonoBehaviour
{
    private PlayerRespawnHandler respawnHandler;

    private void Start()
    {
        respawnHandler = GetComponentInParent<PlayerRespawnHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            var effectPlayer = FindObjectOfType<EffectPlayer>();
            effectPlayer?.PlayEffect(transform.position, "Body");
            
            respawnHandler?.TriggerRespawn();
        }
    }
}
