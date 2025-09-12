using UnityEngine;

public class MusicZone : MonoBehaviour
{
    [Header("Refs")]
    public BackgroundMusicTrigger backgroundMusic;
    public string playerTag = "Player";

    [Header("Behavior")]
    public bool revertOnExit = true;          // beim Verlassen zurück zu background2
    public bool switchToAmbientOnEnter = true; // beim Betreten Dark1 an, background2 aus

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;

        if (switchToAmbientOnEnter)
        {
            backgroundMusic?.StopWithFade();
            DarkAmbientMusicController.Instance?.ResumeWithFade();
        }
        else
        {
            DarkAmbientMusicController.Instance?.StopWithFade();
            backgroundMusic?.StartMusic();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsPlayer(other) || !revertOnExit) return;

        DarkAmbientMusicController.Instance?.StopWithFade();
        backgroundMusic?.StartMusic();
    }

    private bool IsPlayer(Collider other)
    {
        return other.CompareTag(playerTag) ||
               (other.attachedRigidbody && other.attachedRigidbody.CompareTag(playerTag));
    }
}
