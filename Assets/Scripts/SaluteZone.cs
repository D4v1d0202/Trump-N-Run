using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaluteZone : MonoBehaviour
{
    private bool playerInZone = false;
    private bool hasSaluted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            hasSaluted = false;
            Debug.Log("Player ist in der Salutier-Zone");
        }
    }

    void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        if (!hasSaluted)
        {
            DecisionManager.Instance.SetDecision("Saluted", false);
            Debug.Log("Player hat NICHT salutiert!");
        }

        DecisionManager.Instance.HandleSaluteBarriers();
        playerInZone = false;
    }
}

void Update()
{
    if (playerInZone && Input.GetKeyDown(KeyCode.P))
    {
        hasSaluted = true;
        DecisionManager.Instance.SetDecision("Saluted", true);
        Debug.Log("Player hat salutiert!");

        DecisionManager.Instance.HandleSaluteBarriers();
    }
}
}