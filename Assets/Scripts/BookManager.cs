using UnityEngine;

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
}
