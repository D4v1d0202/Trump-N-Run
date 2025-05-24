using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 defaultPosition;
    private bool triggered = false;
    private float triggeredTimer = 0;
    private bool triggerTimerReached = false;
    private bool falling = false;
    private bool fallingTimerReached = false;
    private float fallingTimer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultPosition = transform.position;
    }

    void Update()
    {
        if (triggered)
        {
            if (!triggerTimerReached)
                triggeredTimer += Time.deltaTime;

            if (!triggerTimerReached && triggeredTimer > 2) // waiting 2 secs
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation; //re-freeze
                falling = true;

                triggered = false;
                triggerTimerReached = true;
                fallingTimer = 0;
                fallingTimerReached = false;
            }
        }

        if (falling)
        {
            if (!fallingTimerReached)
                fallingTimer += Time.deltaTime;

            if (!fallingTimerReached && fallingTimer > 3) // falling 3 secs
            {
                falling = false;
                transform.position = defaultPosition;
                rb.constraints = RigidbodyConstraints.FreezeAll;

                fallingTimerReached = true;
                triggeredTimer = 0;
                triggerTimerReached = false;
            }
        }

        //Debug.Log(falling);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "PlayerObj")
        {
            triggered = true;
        }
    }
}
