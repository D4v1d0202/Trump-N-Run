using UnityEngine;

public class Poster2Zone : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip tearStartClip;
    public AudioClip tearFollowClip;
    public AudioClip keepClip; 
    [Range(0f, 1f)] public float volume = 0.8f;
    public float followDelay = 0f;    // optional

    [Header("UI")]
    public GameObject posterCanvas; // Nur das Canvas (kein Text mehr)

    [Header("Poster Variants")]
    public GameObject poster2;      // Das ursprüngliche Poster
    public GameObject poster2Torn;  // Das zerrissene Poster

    private bool playerInZone = false;
    private bool hasInteracted = false;
    private bool decisionMade = false;

    void Start()
    {
        if (posterCanvas != null)
            posterCanvas.SetActive(false);
        if (poster2 != null)
            poster2.SetActive(true);
        if (poster2Torn != null)
            poster2Torn.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
            hasInteracted = false;
            if (!decisionMade && posterCanvas != null)
            {
                posterCanvas.SetActive(true);
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = false;
            if (posterCanvas != null)
                posterCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInZone && !decisionMade && !hasInteracted && Input.GetMouseButtonDown(0)) // Linksklick
        {
            hasInteracted = true;
            decisionMade = true;
            if (poster2 != null) poster2.SetActive(false);
            if (poster2Torn != null) poster2Torn.SetActive(true);
            DecisionManager.Instance.HandlePoster2Barriers(true); // Rechte Blockade öffnen
            if (posterCanvas != null)
                posterCanvas.SetActive(false);

            // AUDIO bei zerreißen
            if (audioSource != null)
                StartCoroutine(PlaySequence(tearStartClip, tearFollowClip));
        }

        if (playerInZone && !decisionMade && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted = true;
            decisionMade = true;
            if (poster2 != null) poster2.SetActive(true);
            if (poster2Torn != null) poster2Torn.SetActive(false);
            DecisionManager.Instance.HandlePoster2Barriers(false); // Linke Blockade öffnen
            if (posterCanvas != null)
                posterCanvas.SetActive(false);

            // AUDIO heile lassen
            if (audioSource != null && keepClip != null)
                audioSource.PlayOneShot(keepClip, volume);
        }
    }

    // Coroutine für AUDIO; da zwei Clips direkt hintereinander
    private System.Collections.IEnumerator PlaySequence(AudioClip first, AudioClip second)
    {
        if (audioSource == null) yield break;

        if (first != null)
        {
            audioSource.PlayOneShot(first, volume);
            yield return new WaitForSeconds(first.length + followDelay);
        }

        if (second != null)
            audioSource.PlayOneShot(second, volume);
    }
}
