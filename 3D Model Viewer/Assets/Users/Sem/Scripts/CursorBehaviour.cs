using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorBehaviour : MonoBehaviour
{
    public Sprite pointerSprite;
    public Sprite openHandSprite;
    public Sprite grabbingHandSprite;

    private Image image;
    void Start()
    {
        Cursor.visible = false;
        // Add a SpriteRenderer to this GameObject if it doesn't exist
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

        // Check for mouse clicks
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            image.sprite = grabbingHandSprite;
        }
        // Raycast to detect interactable objects
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Button"))
            {
                image.sprite = pointerSprite;
                return;
            }
        }

        else
        {
            image.sprite = openHandSprite;
        }
    }

}
