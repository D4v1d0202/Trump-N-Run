using UnityEngine;
using System.Collections;

public class DarkAmbientMusicController : MonoBehaviour
{
    public static DarkAmbientMusicController Instance { get; private set; }

    [Header("Audio")]
    public AudioSource source;
    [Range(0f, 1f)] public float targetVolume = 0.15f;

    [Header("Fades (Sekunden)")]
    public float fadeInDuration = 2f;
    public float fadeOutDuration = 2f;

    private Coroutine fadeRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (source == null) source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.loop = true;
            source.playOnAwake = false;
        }
    }

    private void Start()
    {
        if (source == null || source.clip == null) return;

        source.volume = 0f;
        source.Play();
        FadeTo(targetVolume, fadeInDuration);
    }

    public void StopWithFade()
    {
        FadeTo(0f, fadeOutDuration, stopAfter: true);
    }

    public void ResumeWithFade()
    {
        if (source == null || source.clip == null) return;
        if (!source.isPlaying) source.Play();
        FadeTo(targetVolume, fadeInDuration);
    }

    private void FadeTo(float to, float duration, bool stopAfter = false)
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeRoutine(to, duration, stopAfter));
    }

    private IEnumerator FadeRoutine(float to, float duration, bool stopAfter)
    {
        float from = source.volume;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(from, to, duration <= 0f ? 1f : t / duration);
            yield return null;
        }
        source.volume = to;

        if (stopAfter)
            source.Stop();
    }
}
