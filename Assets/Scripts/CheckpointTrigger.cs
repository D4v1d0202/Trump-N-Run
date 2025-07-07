using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointIndex = 10; // sollte höher sein als die positionsbasierten (1–3)
    public Transform checkpointLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager rm = FindObjectOfType<RespawnManager>();
            if (rm != null)
            {
                rm.SetCheckpoint(checkpointLocation, checkpointIndex);
            }
        }
    }
}