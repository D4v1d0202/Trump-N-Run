using UnityEngine;

public class Poster1Zone : MonoBehaviour
{
    private bool playerInZone = false;
    private bool hasSaluted = false;
    private bool decisionMade = false;

    [Header("UI")]
    public GameObject posterCanvas; // Canvas mit Text
    public TMPro.TextMeshProUGUI infoText; // UI-Text für die Meldung

    [Header("Audio")]
    public AudioSource audioSource;   
    public AudioClip saluteClip;      // Sound salutieren
    public AudioClip refuseClip;      // Sound ablehnen

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
            hasSaluted = false;
            if (!decisionMade && posterCanvas != null && infoText != null)
            {
                posterCanvas.SetActive(true);
                infoText.text = "Drücke P zum Salutieren oder E, wenn du es nicht tun willst. Wähle so deinen Weg";
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
        if (playerInZone && !decisionMade && Input.GetKeyDown(KeyCode.P))
        {
            hasSaluted = true;
            decisionMade = true;
            DecisionManager.Instance.HandlePoster1Barriers(true);
            if (posterCanvas != null)
                posterCanvas.SetActive(false);

            // SOUND ABSPIELEN SALUTIEREN
            if (audioSource != null && saluteClip != null)
                audioSource.PlayOneShot(saluteClip); //
        }
        if (playerInZone && !decisionMade && !hasSaluted && Input.GetKeyDown(KeyCode.E))
        {
            decisionMade = true;
            DecisionManager.Instance.HandlePoster1Barriers(false);
            if (posterCanvas != null)
                posterCanvas.SetActive(false);

            // SOUND ABLEHNUNG
            if (audioSource != null && refuseClip != null)
                audioSource.PlayOneShot(refuseClip); //
        }
    void Start()
    {
        if (posterCanvas != null)
            posterCanvas.SetActive(false);
    }
    }
}
