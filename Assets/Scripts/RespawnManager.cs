using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform player;
    public Transform playerStartPoint;
    public Transform playerCheckpoint1;
    public Transform playerCheckpoint2;

    private Transform currentRespawnPoint;

    void Start()
    {
        currentRespawnPoint = playerStartPoint;
    }

    void Update()
    {
        if (player.position.x < playerCheckpoint2.position.x)
        {
            currentRespawnPoint = playerCheckpoint2;
        }
        else if (player.position.x < playerCheckpoint1.position.x)
            {
                currentRespawnPoint = playerCheckpoint1;
            }

        if (Input.GetKeyDown(KeyCode.R))
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
    }
}