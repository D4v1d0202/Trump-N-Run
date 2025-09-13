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
    private MeshRenderer bookRenderer;

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
    }

    void Update()
    {
        // Buch einsammeln
        if (isPlayerNearBook && !bookTaken && Input.GetKeyDown(KeyCode.G))
        {
            if (bookRenderer != null)
            {
                bookRenderer.enabled = false;
            }
            bookTaken = true;
            Debug.Log("Buch eingesammelt!");

            // AUDIO einsammeln
            if (audioSource != null && pickUpClip != null)
            {
                audioSource.PlayOneShot(pickUpClip, volume);
            }
        }

        // Buch an Position 1 oder 2 erscheinen lassen
        if (bookTaken)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                bookObject.transform.position = position1.position;
                if (bookRenderer != null)
                {
                    bookRenderer.enabled = true;
                }
                bookTaken = false;
                Debug.Log("Buch an Position 1 platziert!");

                // AUDIO Position 1
                if (audioSource != null)
                    StartCoroutine(PlaySequence(placeStartClip, placeVoiceClip, placeFollowDelay));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                bookObject.transform.position = position2.position;
                if (bookRenderer != null)
                {
                    bookRenderer.enabled = true;
                }
                bookTaken = false;
                Debug.Log("Buch an Position 2 platziert!");

                // AUDIO Position 2
                if (audioSource != null)
                    StartCoroutine(PlaySequence(trashStartClip, trashVoiceClip, trashFollowDelay));
            }
        }
    }

    // Trigger-Events für die Nähe zum Buch
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearBook = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearBook = false;
        }
    }

    // Coroutine zum sicheren apbsielen
    private IEnumerator PlaySequence(AudioClip first, AudioClip second, float extraDelay)
    {
        if (audioSource == null) yield break;

        if (first != null)
        {
            audioSource.PlayOneShot(first, volume);
            yield return new WaitForSeconds((first.length > 0f ? first.length : 0f) + Mathf.Max(0f, extraDelay));
        }

        if (second != null)
        {
            audioSource.PlayOneShot(second, volume);
        }
    }

}
