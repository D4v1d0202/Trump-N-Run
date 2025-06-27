using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaluteZone : MonoBehaviour
{
    private bool playerInZone = false;
    public bool hasSaluted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            Debug.Log("Player ist in der Salutier-Zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            Debug.Log("Player hat die Salutier-Zone verlassen");
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.P))
        {
            hasSaluted = true;
            Debug.Log("Player hat salutiert!");
        }
    }
}
