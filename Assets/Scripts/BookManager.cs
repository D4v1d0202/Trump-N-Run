using UnityEngine;
using System.Collections;

public class BookManager : MonoBehaviour
{
    [Header("Das Buch-Objekt")]
    public GameObject bookObject;

    [Header("Ziel-Positionen für das Buch")]
    public Transform position1;
    public Transform position2;

    private bool isPlayerNearBook = false;
    private bool bookTaken = false;
    private bool decisionMade = false; // <--- NEU
    private MeshRenderer bookRenderer;

    [Header("UI")]
    public GameObject bookCanvas; // Canvas zum Anzeigen

    // AUDIO
    [Header("Audio")]
    public AudioSource audioSource;
    [Range(0f, 1f)] public float volume = 0.8f;

    [Header("Audio beim Aufheben (Taste G)")]
    public AudioClip pickUpClip;

    [Header("Audio beim Ablegen (Taste 1)")]
    public AudioClip placeStartClip;    // Buch
    public AudioClip placeVoiceClip;    // Voice
    public float placeFollowDelay = 0f;

    [Header("Audio beim Wegwerfen (Taste 2)")]
    public AudioClip trashStartClip;    // Buch
    public AudioClip trashVoiceClip;    // Voice
    public float trashFollowDelay = 0f;
    // 

    void Start()
    {
        bookRenderer = bookObject.GetComponent<MeshRenderer>();
        if (bookRenderer == null)
        {
            Debug.LogError("Kein MeshRenderer am Buch-Objekt gefunden!");
        }

        if (bookCanvas != null)
            bookCanvas.SetActive(false);
    }

    void Update()
    {
        // Buch einsammeln
        if (isPlayerNearBook && !bookTaken && !decisionMade && Input.GetKeyDown(KeyCode.G))
        {
            if (bookRenderer != null)
                bookRenderer.enabled = false;

            bookTaken = true;
            Debug.Log("Buch eingesammelt!");

            if (bookCanvas != null)
                bookCanvas.SetActive(false);

            if (audioSource != null && pickUpClip != null)
                audioSource.PlayOneShot(pickUpClip, volume);
        }

        // Buch an Position 1 oder 2 erscheinen lassen
        if (bookTaken && !decisionMade)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlaceBook(position1);
                decisionMade = true;

                if (audioSource != null)
                    StartCoroutine(PlaySequence(placeStartClip, placeVoiceClip, placeFollowDelay));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlaceBook(position2);
                decisionMade = true;

                if (audioSource != null)
                    StartCoroutine(PlaySequence(trashStartClip, trashVoiceClip, trashFollowDelay));
            }

        }
    }

    private void PlaceBook(Transform targetTransform)
    {
        bookObject.transform.SetPositionAndRotation(targetTransform.position, targetTransform.rotation);

        if (bookRenderer != null)
            bookRenderer.enabled = true;

        bookTaken = false;
        Debug.Log("Buch platziert!");

        if (bookCanvas != null)
            bookCanvas.SetActive(false); // sicherstellen, dass es aus bleibt
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearBook = true;

            if (!bookTaken && !decisionMade && bookCanvas != null)
                bookCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearBook = false;

            if (bookCanvas != null)
                bookCanvas.SetActive(false);
        }
    }

    private IEnumerator PlaySequence(AudioClip first, AudioClip second, float extraDelay)
    {
        if (audioSource == null) yield break;

        if (first != null)
        {
            audioSource.PlayOneShot(first, volume);
            yield return new WaitForSeconds((first.length > 0f ? first.length : 0f) + Mathf.Max(0f, extraDelay));
        }

        if (second != null)
            audioSource.PlayOneShot(second, volume);
    }
}
