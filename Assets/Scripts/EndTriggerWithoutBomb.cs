using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTriggerWithoutBomb : MonoBehaviour
{
  public GameObject player;
    public Camera mainCamera;
    public Transform cameraTarget;
    public AudioSource audioSource;
    public AudioClip noBombClip;
    [Range(0f, 1f)] public float volume = 1f;

    private PlayerMovement playerMovement;
    private bool triggered = false;

    void Start()
    {
        if (player != null)
            playerMovement = player.GetComponent<PlayerMovement>();
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || other.gameObject != player) return;
        if (playerMovement == null || playerMovement.GetGotBomb()) return;

        triggered = true;

        playerMovement.enabled = false;

        if (mainCamera != null && cameraTarget != null)
        {
            mainCamera.transform.SetParent(null);
            mainCamera.transform.position = cameraTarget.position;
            mainCamera.transform.rotation = cameraTarget.rotation;
        }

        if (audioSource != null && noBombClip != null)
            audioSource.PlayOneShot(noBombClip, volume);
    }
}
