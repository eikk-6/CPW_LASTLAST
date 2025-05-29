using UnityEngine;
using System.Collections;

public class PlayerRespawnHandler : MonoBehaviour
{
    private bool isInvulnerable = false;
    private bool isRespawning = false;
    public float invulnerabilityDuration = 2f;

    private Renderer[] renderers;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void TriggerRespawn()
    {
        if (!isInvulnerable && !isRespawning)
        {
            StartCoroutine(HandleRespawn());
        }
    }

    private IEnumerator HandleRespawn()
    {
        isRespawning = true;

        // "Respawn" 태그를 가진 오브젝트 찾기
        GameObject respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.transform.position;
            transform.rotation = respawnPoint.transform.rotation;
        }
        else
        {
            Debug.LogWarning("[PlayerRespawnHandler] Respawn 태그가 달린 오브젝트를 찾을 수 없습니다.");
        }

        StartCoroutine(InvulnerabilityTimer());

        yield return null;
        isRespawning = false;
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        SetRenderersAlpha(0.5f); // 반투명

        yield return new WaitForSeconds(invulnerabilityDuration);

        isInvulnerable = false;
        SetRenderersAlpha(1f); // 원래대로
    }

    private void SetRenderersAlpha(float alpha)
    {
        foreach (var rend in renderers)
        {
            if (rend.material.HasProperty("_Color"))
            {
                Color c = rend.material.color;
                c.a = alpha;
                rend.material.color = c;
            }
        }
    }
}
