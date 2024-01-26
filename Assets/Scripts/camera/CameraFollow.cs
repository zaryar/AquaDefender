using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.5f; // This is now a time factor
    public float anticipation = 4.0f;
    private float zoomSpeed = 20.0f; // Speed of the camera zoom
    private float minZoom = 15.0f; // Minimum zoom distance
    private float maxZoom = 25.0f; // Maximum zoom distance
    Vector3 offset;
    Vector3 lastTargetPosition;
    Vector3 velocity = Vector3.zero; // This is needed for SmoothDamp

    private void Start()
    {
        offset = transform.position - target.position;
        lastTargetPosition = target.position;
    }

    private void LateUpdate()
    {
        Vector3 directionOfMovement = (target.position - lastTargetPosition).normalized;
        Vector3 anticipatedPosition = target.position + directionOfMovement * anticipation;

        // Add zoom functionality
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zoomChange = scroll * zoomSpeed;
        offset -= offset.normalized * zoomChange;
        float currentZoom = offset.magnitude;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        offset = offset.normalized * currentZoom;

        Vector3 targetCamPos = anticipatedPosition + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetCamPos, ref velocity, smoothing);
        lastTargetPosition = target.position;
    }
}