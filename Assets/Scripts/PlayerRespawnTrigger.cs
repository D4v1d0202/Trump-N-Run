using UnityEngine;

public class PlayerRespawnTrigger : MonoBehaviour
{
    public RespawnManager respawnManager;

    private void OnTriggerEnter(Collider other)
    {
        if (respawnManager != null && other.CompareTag("Respawn Trigger"))
        {
            respawnManager.RespawnPlayer();
        }
    }
}
