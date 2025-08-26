using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform pivot;

    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float rotationDamping = 5f;

    [Header("Pan Settings")]
    [SerializeField] float panSpeed = 0.01f;
    [SerializeField] float panDamping = 12f;

    [Header("Zoom Settings")]
    [SerializeField] float zoomSpeed = 2f;
    [SerializeField] float minZoom = 2f;
    [SerializeField] float maxZoom = 20f;

    [Header("Reset Settings")]
    [SerializeField] float resetDuration = 1f;

    
    private Vector3 startPivotPos;
    private Quaternion startPivotRot;
    private Vector2 panVelocity;
    private Vector2 rotationVelocity;
    private float currentXRotation;
    private bool resetting = false;

    // Reset interpolation
    private float resetTimer = 0f;
    private Vector3 resetStartPivotPos;
    private Quaternion resetStartPivotRot;

    void Start()
    {
        // determine startposition for reset
        startPivotPos = pivot.position;
        startPivotRot = pivot.rotation;

        currentXRotation = pivot.localEulerAngles.x;
        if (currentXRotation > 180f) currentXRotation -= 360f;
    }

    void Update()
    {
        if (resetting)
        {
            HandleReset();
            return;
        }

        HandleResetInput();
        HandleRotation();
        HandlePan();
        HandleZoom();
    }

    #region Reset
    private void HandleResetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            resetting = true;
            resetTimer = 0f;

            resetStartPivotPos = pivot.position;
            resetStartPivotRot = pivot.rotation;


            rotationVelocity = Vector2.zero;
            panVelocity = Vector2.zero;
        }
    }

    private void HandleReset()
    {
        resetTimer += Time.deltaTime;
        float t = Mathf.Clamp01(resetTimer / resetDuration);
        float smoothT = t * t * (3f - 2f * t); // smoothstep

        // Interpolate positions and rotations
        pivot.position = Vector3.Lerp(resetStartPivotPos, startPivotPos, smoothT);
        pivot.rotation = Quaternion.Slerp(resetStartPivotRot, startPivotRot, smoothT);

        if (t >= 1f)
        {
            resetting = false;
            currentXRotation = pivot.localEulerAngles.x;
            if (currentXRotation > 180f) currentXRotation -= 360f;
        }
    }
    #endregion

    #region Rotation
    private void HandleRotation()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Store raw input scaled by rotationSpeed (degrees per second)
            rotationVelocity.x = mouseX * rotationSpeed;
            rotationVelocity.y = -mouseY * rotationSpeed;
        }

        // Apply yaw (left/right)
        pivot.Rotate(Vector3.up, rotationVelocity.x * Time.deltaTime, Space.World);

        // Apply pitch (up/down)
        currentXRotation += rotationVelocity.y * Time.deltaTime;
        currentXRotation = Mathf.Clamp(currentXRotation, -90f, 90f);

        Vector3 euler = pivot.localEulerAngles;
        euler.x = currentXRotation;
        pivot.localEulerAngles = euler;

        // Smoothly reduce rotation velocity toward zero
        rotationVelocity = Vector2.Lerp(rotationVelocity, Vector2.zero, rotationDamping * Time.deltaTime);
    }

    #endregion

    #region Pan
    private void HandlePan()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            float distance = Mathf.Max(Vector3.Distance(transform.position, pivot.position), 1f);

            panVelocity.x = Input.GetAxis("Mouse X") * panSpeed * transform.localPosition.z * distance;
            panVelocity.y = Input.GetAxis("Mouse Y") * panSpeed * transform.localPosition.z * distance;
        }

        pivot.Translate(panVelocity.x, panVelocity.y, 0, Space.Self);
        panVelocity = Vector2.Lerp(panVelocity, Vector2.zero, panDamping * Time.deltaTime);
    }
    #endregion

    #region Zoom
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Vector3 dir = transform.localPosition.normalized;
            float distance = transform.localPosition.magnitude;

            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom);

            transform.localPosition = dir * distance;
        }
    }
    #endregion
}
