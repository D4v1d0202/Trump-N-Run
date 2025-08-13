using UnityEngine;
using System.Collections;

public class SpeechTrigger : MonoBehaviour
{
    [Header("Speech Settings")]
    public AudioSource speechSource;
    public GameObject blockade;

    [Header("Background Music Control")]
    public BackgroundMusicTrigger bgMusic;

    private bool hasPlayed = false;

    private void Start()
    {
        if (blockade != null) blockade.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            if (bgMusic != null) bgMusic.StopWithFade();

            if (blockade != null) blockade.SetActive(true);

            speechSource.Play();

            StartCoroutine(RemoveBlockadeWhenSpeechEnds());

            hasPlayed = true;
        }
    }

    private IEnumerator RemoveBlockadeWhenSpeechEnds()
    {
        while (speechSource.isPlaying)
        {
            yield return null; 
        }
        if (blockade != null) blockade.SetActive(false); 
    }
}
