using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaluteZone : MonoBehaviour
{
    private bool playerInZone = false;
    private bool hasSaluted = false;
    public GameObject saluteCanvas;

    void Start()
    {
        if (saluteCanvas != null)
            saluteCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInZone = true;
            hasSaluted = false;

            if (saluteCanvas != null)
                saluteCanvas.SetActive(true);

            Debug.Log("Player ist in der Salutier-Zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInZone = false;
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.P))
        {
            hasSaluted = true;

            if (saluteCanvas != null)
                saluteCanvas.SetActive(false);

            Debug.Log("Player hat salutiert!");
        }
    }
}