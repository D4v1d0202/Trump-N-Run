using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuftragDialogueManager : MonoBehaviour
{
    public GameObject canvas;

    public string triggeringObjectName = "Auftrag Interaction Trigger";

    private void Start()
    {
        if (canvas != null)
            canvas.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == triggeringObjectName)
        {
            if (canvas != null)
                canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == triggeringObjectName)
        {
            if (canvas != null)
                canvas.SetActive(false);
        }
    }
}
