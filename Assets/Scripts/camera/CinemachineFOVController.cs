using UnityEngine;
using Cinemachine;

public class CinemachineFOVController : MonoBehaviour
{
    public float zoomSpeed = 10f;
    private float maxFOV = 30f;
    private float minFOV = 3f;
    private CinemachineVirtualCamera vcam;
    public float mouseSensitivity = 0.1f;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        CinemachineFramingTransposer composer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        composer.m_CameraDistance -= scrollData * zoomSpeed;
        composer.m_CameraDistance = Mathf.Clamp(composer.m_CameraDistance, minFOV, maxFOV);

        // Get the mouse position in viewport coordinates (0 to 1)
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.x /= Screen.width;
        mousePosition.y /= Screen.height;

        // Subtract 0.5 so the offset is centered around the middle of the screen
        mousePosition -= new Vector3(0.5f, 0.5f, 0);

        // Apply the offset to the camera's position
        composer.m_CameraOffset += mousePosition * mouseSensitivity;
    }
}