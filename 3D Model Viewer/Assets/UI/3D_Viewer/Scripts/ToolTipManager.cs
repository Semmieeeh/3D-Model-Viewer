using System;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour 
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    public int tipHeightOfset;
    public int tipWidthOfset;

    // Dit zijn de "Actions" voor het aanroepen van de tooltip. we geven een string en vector2 mee, deze acties kunnen uit elke code weer aangeropen worden.
    public static Action <string , Vector2> OnMouseHover;
    public static Action OnMouseUnfocus;


    // Hier Subscribe we de acties hier boven aan functies zo dat als de actie aangeropen word de functie uitgevoerd word.
    private void OnEnable () {
        OnMouseHover += ShowToolTip;
        OnMouseUnfocus += HideToolTip;
    }
    // En hier UnSubscribe we de acties weer zodat er geen NullReferenceException komen.
    private void OnDisable () {
        OnMouseHover -= ShowToolTip;
        OnMouseUnfocus -= HideToolTip;
    }
    // In Start zetten we de tooltip meteen uit door "HideToolTip" te activeren hier mee zorgen we dat de tooltip altijd ondzichtbaar is bij het starten van de scene.
    private void Start () {
        HideToolTip();
    }
    // Met "ShowToolTip" zetten we de tooltip aan waar hij aan moet, de "ToolTip" geeft door welke text dat hij moet laten zien.
    // Veder zetten we de paneel grote aan de zelfde maat als de text zo dat het er niet raar uit ziet we geven ook aan dat het niet groter mag zijn dan 150 pixels.
    // En dan geven we de locatie door waar hij tevoorschijn moet komen vanuit de muis positie wegeven het ook een ofset mee zodat het niet onder de muis weg valt of van het scherm af.
    private void ShowToolTip (string tip, Vector2 mousePos) { 
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 150 ? 150 : tipText.preferredWidth, tipText.preferredHeight);
        tipWindow.transform.position = new Vector2 (mousePos.x + (tipWindow.sizeDelta.x * tipWidthOfset) , mousePos.y + (tipWindow.sizeDelta.y * tipHeightOfset));
        tipWindow.gameObject.SetActive(true);
        
    }
    // hier zetten we de text vand de tooltip weer naar "default" dus leeg en zetten hem ook weer uit.
    private void HideToolTip () {
        tipText.text = default;
        tipWindow.gameObject.SetActive (false) ;
    }
    
}
