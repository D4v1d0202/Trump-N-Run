using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeechTrigger : MonoBehaviour
{
    public AudioSource speechSource;      
    private bool hasPlayed = false;       

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            speechSource.Play();
            hasPlayed = true;
        }
    }
}

