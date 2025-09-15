using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public string playerTag = "Player";
    public Transform holdPoint;
    public Vector3 holdOffset = new Vector3(0.3f, -0.2f, 0.6f);
    public Vector3 pickedUpScale = new Vector3(0.3f, 0.3f, 0.3f);

    private bool isPickedUp;
    private Vector3 originalScale;
    private Rigidbody rb;
    private Collider[] allColliders;

    void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        allColliders = GetComponentsInChildren<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return;
        if (other.CompareTag(playerTag))
            PickUp();
    }

    void PickUp()
    {
        isPickedUp = true;
        transform.localScale = pickedUpScale *= 0.3f;

        foreach (var col in allColliders)
            col.enabled = false;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (holdPoint != null)
            transform.position = holdPoint.position + holdOffset;
        else
            Debug.LogWarning("HoldPoint not assigned for " + name);
    }

    void LateUpdate()
    {
        if (!isPickedUp || holdPoint == null) return;

        transform.position = holdPoint.position + holdOffset;

        // rotate with player yaw only
        Vector3 forward = holdPoint.forward;
        forward.y = 0;
        if (forward.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(forward);
    }
}
