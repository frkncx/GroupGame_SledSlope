using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform penguin;  // Assign the penguin's transform in the Inspector
    public float smoothSpeed = 5f;  // How smoothly the camera follows
    public Vector3 offset = new Vector3(0f, 0f, -10f);  // Camera offset (z should be negative for 2D)

    private void LateUpdate()
    {
        if (penguin != null)
        {
            // Calculate desired camera position
            Vector3 desiredPosition = penguin.position + offset;

            // Smoothly move the camera towards the penguin
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

}
