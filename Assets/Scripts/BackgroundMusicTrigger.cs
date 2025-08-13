using UnityEngine;

public class BackgroundMusicTrigger : MonoBehaviour
{
    public AudioSource musicSource;
    public float fadeDuration = 1f;

    private float initialVolume;
    private Coroutine fadeCoroutine;
    private bool isPlaying = false;

    private void Start()
    {
        // Lautstärke aus Inspector übernehmen
        initialVolume = musicSource.volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            StartMusic();
        }
    }

    public void StartMusic()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        if (!musicSource.isPlaying) musicSource.Play();
        fadeCoroutine = StartCoroutine(FadeTo(initialVolume));
        isPlaying = true;
    }

    public void StopWithFade()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(0f, stopWhenSilent: true));
        isPlaying = false;
    }

    private System.Collections.IEnumerator FadeTo(float target, bool stopWhenSilent = false)
    {
        float start = musicSource.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(start, target, t / fadeDuration);
            yield return null;
        }
        musicSource.volume = target;

        if (stopWhenSilent && target <= 0f)
            musicSource.Stop();
    }
}
