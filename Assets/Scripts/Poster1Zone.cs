using UnityEngine;

public class Poster1Zone : MonoBehaviour
{
    private bool playerInZone = false;
    private bool hasSaluted = false;
    public GameObject posterCanvas; // Canvas mit Text
    public TMPro.TextMeshProUGUI infoText; // UI-Text für die Meldung

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
            hasSaluted = false;
            if (posterCanvas != null && infoText != null)
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
        if (playerInZone && Input.GetKeyDown(KeyCode.P))
        {
            hasSaluted = true;
            DecisionManager.Instance.HandlePoster1Barriers(true);
            if (posterCanvas != null)
                posterCanvas.SetActive(false);
        }
        if (playerInZone && !hasSaluted && Input.GetKeyDown(KeyCode.E))
        {
            DecisionManager.Instance.HandlePoster1Barriers(false);
            if (posterCanvas != null)
                posterCanvas.SetActive(false);
        }
    void Start()
    {
        if (posterCanvas != null)
            posterCanvas.SetActive(false);
    }
    }
}
