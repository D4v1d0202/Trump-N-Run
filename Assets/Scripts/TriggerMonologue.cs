using UnityEngine;

public class TriggerMonologue : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Keine AudioSource am GameObject 'AudioMonologues' gefunden!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
