using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;                  // Character or object to follow
    public string targetTag = "Player";       // Auto-find tag if target is null

    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(-15, 25, -15);
    public float smoothSpeed = 5f;

    [Header("Zoom Settings (Orthographic Only)")]
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 40f;

    private Camera cam;

    private void Start()
    {
        // Get the camera from a child instead of on this object (CameraRig)
        cam = GetComponentInChildren<Camera>();

        // Auto-find target if not manually assigned
        if (target == null && !string.IsNullOrEmpty(targetTag))
        {
            GameObject found = GameObject.FindGameObjectWithTag(targetTag);
            if (found != null) target = found.transform;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position (offset rotated with the camera)
        Vector3 desiredPosition = target.position + transform.rotation * offset;

        // Smooth follow movement
        transform.position = desiredPosition;

    }

    private void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        if (cam != null && cam.orthographic)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                cam.orthographicSize -= scroll * zoomSpeed;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            }
        }
    }
}
