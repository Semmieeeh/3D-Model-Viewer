using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModelSelector : MonoBehaviour
{
    // Een array van image's waar alle modellen laten zien worden
    public Button[] imageButtons;

    // De selecteer knop om het gekozen model te laden
    public Button confirmButton;

    // Een outline effect om het duidelijk te maken welk model momenteel is geselecteerd
    public Color outlineColor = Color.yellow;
    public float outlineWidth = 4f;

    private Button selectedImageButton;

    void Start()
    {
        // Uit zetten van de selecteer knop zodat er altijd een model geselecteerd moet worden voor dat er iets geladen kan worden
        if (confirmButton != null)
        {
            confirmButton.interactable = false;
        }

        // Voegt een listener toe om de image's van de modellen klikbaar te maken
        foreach (Button btn in imageButtons)
        {
            btn.onClick.AddListener(() => OnImageSelected(btn));
        }

        // listner voor de selecteer button
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(LoadNextScene);
        }
    }

    void OnImageSelected(Button selectedButton)
    {
        // Zet de outline van de image uit als er op een nieuwe image geclickt wordt
        if (selectedImageButton != null)
        {
            Outline previousOutline = selectedImageButton.GetComponent<Outline>();
            if (previousOutline != null)
            {
                previousOutline.enabled = false;
            }
        }

        // Selecteerd de nieuwe image
        selectedImageButton = selectedButton;

        // Zet de outline op de nieuwe image aan
        Outline outline = selectedButton.GetComponent<Outline>();
        if (outline == null)
        {
            outline = selectedButton.gameObject.AddComponent<Outline>();
            outline.effectColor = outlineColor;
            outline.effectDistance = new Vector2(outlineWidth, outlineWidth);
        }
        outline.enabled = true;

        // Zet de selecteer button aan
        if (confirmButton != null)
        {
            confirmButton.interactable = true;
        }
    }

    void LoadNextScene()
    {
        if (selectedImageButton != null)
        {
            // Slaat de index of naam op van de image voor gebruik in de volgende scene
            int selectedIndex = System.Array.IndexOf(imageButtons, selectedImageButton);
            PlayerPrefs.SetInt("SelectedImageIndex", selectedIndex);

            // laad de volgende scene
            SceneManager.LoadScene("NextSceneName");
        }
    }
}
