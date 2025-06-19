using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 LastPosition;
    public Vector3 Velocity;

    private void Start()
    {
        LastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Velocity = (transform.position - LastPosition) / Time.deltaTime;
        LastPosition = transform.position;
    }
}
