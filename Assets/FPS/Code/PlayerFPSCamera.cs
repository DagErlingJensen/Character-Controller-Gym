using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPSCamera : MonoBehaviour
{
    public float MouseSensitivity = 100;

    private float xRotation = 0;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // The rotation around the x-axis is usually the inverse of the mouse y input
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamps the rotation to 90 degrees in each direction

        // Rotates the camera locally around its x-axis
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // Rotates the Player-transform around the up- / y-axis
        transform.Rotate(Vector3.up * mouseX);
    }
}
