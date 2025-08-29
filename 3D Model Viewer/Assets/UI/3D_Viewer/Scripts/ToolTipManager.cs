using System;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour 
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    public int tipHeightOfset;
    public int tipWidthOfset;


    public static Action <string , Vector2> OnMouseHover;
    public static Action OnMouseUnfocus;
   
    private void OnEnable () {
        OnMouseHover += ShowToolTip;
        OnMouseUnfocus += HideToolTip;
    }

    private void OnDisable () {
        OnMouseHover -= ShowToolTip;
        OnMouseUnfocus -= HideToolTip;
    }

    private void Start () {
        HideToolTip();
    }

    private void ShowToolTip (string tip, Vector2 mousePos ) { 
        tipText.text = tip;
        tipWindow.sizeDelta= new Vector2(tipText.preferredWidth > 150 ? 150 : tipText.preferredWidth, tipText.preferredHeight);
        tipWindow.transform.position = new Vector2 (mousePos.x + (tipWindow.sizeDelta.x * tipWidthOfset) , mousePos.y + (tipWindow.sizeDelta.y * tipHeightOfset));
        tipWindow.gameObject.SetActive( true );
        
    }

    private void HideToolTip () {
        tipText.text = default;
        tipWindow.gameObject.SetActive ( false) ;
    }
    
}
