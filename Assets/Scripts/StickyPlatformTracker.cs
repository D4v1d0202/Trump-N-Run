using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StickyPlatformTracker : MonoBehaviour
{
    private MovingPlatform currentPlatform;
    private Rigidbody rb;
    public bool isOnMovingPlatform = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovingPlatform>(out var platform))
        {
            currentPlatform = platform;
            isOnMovingPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovingPlatform>() == currentPlatform)
        {
            currentPlatform = null;
            isOnMovingPlatform = false;
        }
    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            rb.position += currentPlatform.Velocity * Time.fixedDeltaTime;
        }
    }
}
