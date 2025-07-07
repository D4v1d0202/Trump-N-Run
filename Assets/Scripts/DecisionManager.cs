using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public static DecisionManager Instance;

    private Dictionary<string, bool> decisions = new Dictionary<string, bool>();

    [Header("Optional: Barrieren für Salute-Entscheidung")]
    public GameObject leftBlocker;
    public GameObject rightBlocker;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initiale Barrierensteuerung (nur wenn Objekte zugewiesen sind)
        HandleSaluteBarriers();
    }

    public void SetDecision(string key, bool value)
    {
        decisions[key] = value;
        Debug.Log($"Entscheidung gesetzt: {key} = {value}");
    }

    public bool GetDecision(string key)
    {
        if (decisions.TryGetValue(key, out bool value))
        {
            return value;
        }

        Debug.LogWarning($"Entscheidung wurde nicht gesetzt: {key}");
        return false;
    }

    public void HandleSaluteBarriers()
{
    if (leftBlocker == null || rightBlocker == null)
    {
        Debug.LogWarning("Blocker nicht zugewiesen!");
        return;
    }

    leftBlocker.SetActive(false);  // sicherheitshalber aus
    rightBlocker.SetActive(false); // sicherheitshalber aus

    if (GetDecision("Saluted"))
    {
        leftBlocker.SetActive(true);
        Debug.Log("Linker Weg blockiert (Saluted = true)");
    }
    else
    {
        rightBlocker.SetActive(true);
        Debug.Log("Rechter Weg blockiert (Saluted = false)");
    }
}
}