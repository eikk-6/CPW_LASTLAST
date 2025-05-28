using UnityEngine;
using Unity.Netcode;

public class PlayerRespawner : NetworkBehaviour
{
    private Vector3 savedRespawnPos = Vector3.zero;

    public void SaveRespawnPosition(Vector3 pos)
    {
        savedRespawnPos = pos;
        Debug.Log($"[PlayerRespawner] ������ ��ġ �����: {savedRespawnPos}");
    }

    [ServerRpc(RequireOwnership = false)]
    public void RespawnAtSavedLocationServerRpc()
    {
        if (savedRespawnPos != Vector3.zero)
        {
            transform.position = savedRespawnPos;
        }
    }
}
