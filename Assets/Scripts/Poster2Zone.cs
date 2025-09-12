using UnityEngine;

public class Poster2Zone : MonoBehaviour
{
    private bool playerInZone = false;
    private bool hasInteracted = false;
    public GameObject posterCanvas; // Canvas mit Text
    public TMPro.TextMeshProUGUI infoText; // UI-Text für die Meldung
    public GameObject poster2; // Das ursprüngliche Poster
    public GameObject poster2Torn; // Das zerrissene Poster

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
            if (posterCanvas != null && infoText != null)
            {
                posterCanvas.SetActive(true);
                infoText.text = "Klicke mit der linken Maustaste, um das Poster zu zerreißen oder drücke E, um es zu heile zu lassen.";
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
        if (playerInZone && !hasInteracted && Input.GetMouseButtonDown(0)) // Linksklick
        {
            hasInteracted = true;
            if (poster2 != null) poster2.SetActive(false);
            if (poster2Torn != null) poster2Torn.SetActive(true);
            DecisionManager.Instance.HandlePoster2Barriers(true); // Rechte Blockade öffnen
            if (posterCanvas != null)
                posterCanvas.SetActive(false);
        }
        if (playerInZone && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted = true;
            if (poster2 != null) poster2.SetActive(true);
            if (poster2Torn != null) poster2Torn.SetActive(false);
            DecisionManager.Instance.HandlePoster2Barriers(false); // Linke Blockade öffnen
            if (posterCanvas != null)
                posterCanvas.SetActive(false);
        }
    }
}
