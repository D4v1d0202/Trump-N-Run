using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterZone : MonoBehaviour
{
    public PosterInteractable posterInteractable;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            posterInteractable.SetPlayerInZone(true);
            Debug.Log("Player ist in der Poster-Zone: " + posterInteractable.decisionKey);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            posterInteractable.SetPlayerInZone(false);
            Debug.Log("Player hat die Poster-Zone verlassen: " + posterInteractable.decisionKey);
        }
    }
}