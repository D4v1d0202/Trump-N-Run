using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;   // for movement direction
    public Transform playerBody;    // the mesh/body to rotate

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        // Rotate camera fully (horizontal + vertical)
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Rotate orientation for movement
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // Rotate the player body to match yaw
        if (playerBody != null)
{
    Vector3 bodyEuler = playerBody.eulerAngles;
    bodyEuler.y = yRotation + 90f; // adjust based on how your model faces
    playerBody.eulerAngles = bodyEuler;
}
    }
}
