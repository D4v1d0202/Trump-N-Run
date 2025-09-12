using UnityEngine;
using System.Collections;

public class AuftragAudioTrigger : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource source;
    public AudioClip[] clips;
    [Range(0f, 1f)] public float volume = 0.8f;
    public bool randomize = false;

    [Header("Trigger")]
    public string playerTag = "Player";
    public bool oneShotOnly = true;

    [Header("Blockade")]
    public GameObject blockadeParent; 
    private Collider[] blockadeColliders;

    private bool hasPlayed = false;

    private void Awake()
    {
        if (source == null) source = GetComponent<AudioSource>();
        if (source != null) { source.playOnAwake = false; source.loop = false; }

        if (blockadeParent != null)
        {
            // Collider der 4 Wände nehmen
            blockadeColliders = blockadeParent.GetComponentsInChildren<Collider>(true);
            SetBlockade(true); // Startzustand Blockade an HIER AUF FALSE SETZEN WENN ES EUCH WÄHREND DES PROJEKTS AUF DEN SACK GEHEN SOLLTE
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Spieler erkennen über Tag
        bool isPlayer = other.CompareTag(playerTag) ||
                        (other.attachedRigidbody && other.attachedRigidbody.CompareTag(playerTag));
        if (!isPlayer) return;
        if (oneShotOnly && hasPlayed) return;
        if (source == null || clips == null || clips.Length == 0) return;

        // Blockade eingeschaltet
        SetBlockade(true);

        // Clip wählen und abspielen
        var clip = (randomize && clips.Length > 1) ? clips[Random.Range(0, clips.Length)] : clips[0];
        source.PlayOneShot(clip, volume);
        hasPlayed = true;

        // Nach Audioclip Blockade aus
        StartCoroutine(DisableBlockadeWhenDone());
    }

    private IEnumerator DisableBlockadeWhenDone()
    {
        // Warte bis AudioSource nichts mehr abspielt
        while (source != null && source.isPlaying)
            yield return null;

        SetBlockade(false);
    }

    private void SetBlockade(bool on)
    {
        if (blockadeColliders == null) return;
        foreach (var col in blockadeColliders)
        {
            if (col == null) continue;
            if (col.gameObject == this.gameObject) continue;

            col.enabled = on;
        }
    }
}
