using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform player;
    public Transform playerStartPoint;
    public Transform playerCheckpoint1;
    public Transform playerCheckpoint2;
    public Transform playerCheckpoint3;

    private Transform currentRespawnPoint;

    private int currentCheckpointIndex = 0;

    void Start()
    {
        currentRespawnPoint = playerStartPoint;
    }

    void Update()
    {
        // if (player.position.x < playerCheckpoint3.position.x && currentCheckpointIndex < 3)
        // {
        //     currentRespawnPoint = playerCheckpoint3;
        //     currentCheckpointIndex = 3;
        // }
        // else if (player.position.x < playerCheckpoint2.position.x && currentCheckpointIndex < 2)
        // {
        //     currentRespawnPoint = playerCheckpoint2;
        //     currentCheckpointIndex = 2;
        // }
        // else if (player.position.x < playerCheckpoint1.position.x && currentCheckpointIndex < 1)
        // {
        //     currentRespawnPoint = playerCheckpoint1;
        //     currentCheckpointIndex = 1;
        // }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        player.position = currentRespawnPoint.position;
        player.rotation = currentRespawnPoint.rotation;
    }

    public void SetCheckpoint(Transform checkpointTransform, int checkpointIndex)
    {
        if (checkpointIndex > currentCheckpointIndex)
        {
            currentCheckpointIndex = checkpointIndex;
            currentRespawnPoint = checkpointTransform;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn Trigger"))
        {
            RespawnPlayer();
        }
    }*/
}
