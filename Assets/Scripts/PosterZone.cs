using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterZone : MonoBehaviour
{
    public string decisionKey = "ToreDownPoster_Left"; // or "ToreDownPoster_Right"
    public GameObject posterObject;

    private bool playerInZone = false;
    private bool hasTornDown = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            hasTornDown = false;
            Debug.Log("Player ist in der Poster-Zone: " + decisionKey);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            if (!hasTornDown)
            {
                Debug.Log("Player hat das Poster nicht abgerissen: " + decisionKey);
                DecisionManager.Instance.SetDecision(decisionKey, false);
                DecisionManager.Instance.HandlePosterBarriers(decisionKey);
            }
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.O) && !hasTornDown)
        {
            hasTornDown = true;

            if (posterObject != null)
                posterObject.SetActive(false);

            DecisionManager.Instance.SetDecision(decisionKey, true);
            DecisionManager.Instance.HandlePosterBarriers(decisionKey);

            Debug.Log("Poster abgerissen: " + decisionKey);
        }
    }
}