using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Dit zal op knoppen ge zet worden die een tooltip nodig hebben.
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [TextArea]
    public string toolTipText;
    public float waitTime;
    public int tipHeightOfset;
    public int tipWidthOfset;
    public ToolTipManager toolTipManager;

    // In Start zoeken we naar de "TipManager" zodat we niet op elke knop met een tooltip zelf nog weer er in moeten slepen.
    void Start() {
        toolTipManager = GameObject.Find ("TipManager").GetComponent<ToolTipManager> ();
    }
    // Zodra er een "PointerEnter" event word uitgevoed worden alle "Coroutines" gestopt zo dat er geen problemen kunnen komen, daarna starten we de "Coroutine" "StartTimer".
    public void OnPointerEnter (PointerEventData eventData) {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }
    // Zodra we een "PointerExit" event word uitgevoed worden alle "Coroutines" gestopt en ook wordt de actie "OnMouseUnfocus" uitgoevoerd.
    public void OnPointerExit (PointerEventData eventData) {
        StopAllCoroutines();
        ToolTipManager.OnMouseUnfocus();
    }
    // In "ShowMesage" geven we de ofset mee zodat te tooltip altijd goed zichtbaar is en dan roepen we de actie "OnMouseHover" waar we de text mee die gelaten zien moet worden.
    private void ShowMesage () {
        toolTipManager.tipHeightOfset = tipHeightOfset;
        toolTipManager.tipWidthOfset = tipWidthOfset;
        ToolTipManager.OnMouseHover (toolTipText , Input.mousePosition);
    }
    // In deze "IEnumerator" wachten we voor een bepalde tijd waar na dat afgelopen is we de functie "ShowMesage" aanroepen.
    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(waitTime);
        
        ShowMesage();
    }

}
