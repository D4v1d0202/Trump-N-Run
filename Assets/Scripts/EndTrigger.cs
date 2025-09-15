using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
   [Header("References")]
    public GameObject player;
    public Camera mainCamera;
    public Transform cameraTarget;  // Drag a world transform here for camera position/rotation

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip bombClip;      // Sound if player has bomb
    public AudioClip noBombClip;    // Sound if player has no bomb
    [Range(0f, 1f)] public float volume = 1f;

    private PlayerMovement playerMovement;
    private bool triggered = false;

    private void Start()
    {
        if (player == null)
            Debug.LogError("Player not assigned!");
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (player != null)
            playerMovement = player.GetComponent<PlayerMovement>();
        if (cameraTarget == null)
            Debug.LogError("Camera target not assigned!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.gameObject != player) return;

        triggered = true;

        // Disable player movement
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Move camera to target transform
        if (mainCamera != null && cameraTarget != null)
        {
            mainCamera.transform.SetParent(null);
            mainCamera.transform.position = cameraTarget.position;
            mainCamera.transform.rotation = cameraTarget.rotation;
        }

        // Play audio depending on bomb
        if (audioSource != null)
        {
            if (playerMovement != null && playerMovement.GetGotBomb())
                audioSource.PlayOneShot(bombClip, volume);
            else
                audioSource.PlayOneShot(noBombClip, volume);
        }
    }
}
