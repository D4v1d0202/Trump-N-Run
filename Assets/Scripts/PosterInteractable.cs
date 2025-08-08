using UnityEngine;

public class PosterInteractable : MonoBehaviour
{
    public string decisionKey = "ToreDownPoster_Left"; // Im Inspector setzen!
    private bool playerInZone = false;
    private bool hasTornDown = false;

    // Wird vom Trigger gesetzt
    public void SetPlayerInZone(bool inZone)
    {
        playerInZone = inZone;
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.O) && !hasTornDown)
        {
            RemovePoster();
        }
    }

    void OnMouseDown()
    {
        if (playerInZone && !hasTornDown)
        {
            RemovePoster();
        }
    }

    private void RemovePoster()
    {
        hasTornDown = true;
        DecisionManager.Instance.SetDecision(decisionKey, true);
        DecisionManager.Instance.HandlePosterBarriers(decisionKey);
        gameObject.SetActive(false); // oder Destroy(gameObject);
        Debug.Log("Poster abgerissen: " + decisionKey);
    }
}