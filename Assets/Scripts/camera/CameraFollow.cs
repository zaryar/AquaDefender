using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.5f; // This is now a time factor
    public float anticipation = 4.0f;
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
        Vector3 targetCamPos = anticipatedPosition + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetCamPos, ref velocity, smoothing);
        lastTargetPosition = target.position;
    }
}