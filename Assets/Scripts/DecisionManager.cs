using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public static DecisionManager Instance;

    private Dictionary<string, bool> decisions = new Dictionary<string, bool>();

    [Header("Barrieren für Salute-Entscheidung")]
    public GameObject leftBlocker;
    public GameObject rightBlocker;

    public GameObject leftPosterBlocker;
    public GameObject rightPosterBlocker;
    [Header("Blockaden für Poster2")]
    public GameObject leftPoster2Blocker;
    public GameObject rightPoster2Blocker;

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
        rightBlocker.SetActive(false);

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

    public void HandlePosterBarriers(string decisionKey)
    {
        if (leftPosterBlocker == null || rightPosterBlocker == null)
        {
            Debug.LogWarning("Poster-Blocker nicht zugewiesen!");
            return;
        }

        leftPosterBlocker.SetActive(false);
        rightPosterBlocker.SetActive(false);

        bool toreDown = GetDecision(decisionKey);

        if (decisionKey.Contains("Left"))
        {
            if (toreDown)
            {
                leftPosterBlocker.SetActive(true);
                Debug.Log("Poster (Left) abgerissen, rechter Weg offen");
            }
            else
            {
                rightPosterBlocker.SetActive(true);
                Debug.Log("Poster (Left) NICHT abgerissen, rechter Weg zu");
            }
        }
        else if (decisionKey.Contains("Right"))
        {
            if (toreDown)
            {
                rightPosterBlocker.SetActive(true);
                Debug.Log("Poster (Right) abgerissen, linker Weg offen");
            }
            else
            {
                leftPosterBlocker.SetActive(true);
                Debug.Log("Poster (Right) NICHT abgerissen, linker Weg zu");
            }
        }
    }

    public void HandlePoster1Barriers(bool saluted)
    {
        if (leftPosterBlocker == null || rightPosterBlocker == null)
        {
            Debug.LogWarning("Poster-Blocker nicht zugewiesen!");
            return;
        }

        // Beide Blocker erstmal deaktivieren
        leftPosterBlocker.SetActive(false);
        rightPosterBlocker.SetActive(false);

        if (saluted)
        {
            // Linke Blockade offen, rechte zu
            leftPosterBlocker.SetActive(true);
            rightPosterBlocker.SetActive(false);
            Debug.Log("Salutiert vor Poster 1: Linke offen, rechte zu");
        }
        else
        {
            // Rechte Blockade offen, linke zu
            leftPosterBlocker.SetActive(false);
            rightPosterBlocker.SetActive(true);
            Debug.Log("Nicht salutiert vor Poster 1: Linke zu, rechte offen");
        }
    }

    public void HandlePoster2Barriers(bool torn)
    {
        if (leftPoster2Blocker == null || rightPoster2Blocker == null)
        {
            Debug.LogWarning("Poster2-Blocker nicht zugewiesen!");
            return;
        }

        leftPoster2Blocker.SetActive(false);
        rightPoster2Blocker.SetActive(false);

        if (torn)
        {
            // Rechte Blockade zu, linke offen
            leftPoster2Blocker.SetActive(false);
            rightPoster2Blocker.SetActive(true);
            Debug.Log("Poster2 zerrissen: Linke offen, rechte zu");
        }
        else
        {
            // Linke Blockade zu, rechte offen
            leftPoster2Blocker.SetActive(true);
            rightPoster2Blocker.SetActive(false);
            Debug.Log("Poster2 nicht zerrissen: Linke zu, rechte offen");
        }
    }
}