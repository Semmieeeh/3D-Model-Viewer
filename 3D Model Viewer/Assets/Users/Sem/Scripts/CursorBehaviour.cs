using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorBehaviour : MonoBehaviour
{
    public Sprite pointerSprite;
    public Sprite openHandSprite;
    public Sprite grabbingHandSprite;
    private Vector3 lastMousePosition;
    private float targetRotation = 0f;
    private float currentRotation = 0f;
    private float rotationSpeed = 20f;
    private float maxRotation = 25f;
    private float movementThreshold = 2f;

    private Image image;
    void Start()
    {
        Cursor.visible = false;
        // Add an image to this GameObject if it doesn't exist
        image = GetComponent<Image>();
        if (image == null)
        {
            image = gameObject.AddComponent<Image>();
        }
        image.sprite = openHandSprite; // default
    }
    void Update()
    {
        // Make the cursor follow the mouse
        transform.position = Input.mousePosition;
        HandleRotation();
        // Check for mouse clicks
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            image.sprite = grabbingHandSprite;
        }
        else
        {
            image.sprite = openHandSprite;
        }
        // UI raycast to check which UI element we're over
        if (EventSystem.current != null)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Button"))
                {
                    image.sprite = pointerSprite;
                    return;
                }
            }
        }
        else
        {
            image.sprite = openHandSprite;
        }

    }

    void HandleRotation()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        float deltaX = mouseDelta.x;

        // Only rotate if horizontal movement is above threshold
        if (Mathf.Abs(deltaX) > movementThreshold)
        {
            targetRotation = Mathf.Clamp(deltaX, -1f, 1f) * maxRotation;
        }
        else
        {
            targetRotation = 0f;
        }

        // Smoothly rotate towards target
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, 0f, -currentRotation);

        lastMousePosition = Input.mousePosition;
    }

}
