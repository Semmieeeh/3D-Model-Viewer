using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [TextArea]
    public string toolTipText;
    public float waitTime;
    public int tipHeightOfset;
    public int tipWidthOfset;
    public ToolTipManager toolTipManager;

    void Start() {

        toolTipManager = GameObject.Find ("TipManager").GetComponent<ToolTipManager> ();
    }

    public void OnPointerEnter ( PointerEventData eventData ) {
        StopAllCoroutines();
        StartCoroutine( StartTimer() );
    }

    public void OnPointerExit ( PointerEventData eventData ) {
        StopAllCoroutines();
        ToolTipManager.OnMouseUnfocus();
    }

    private void ShowMesage () {
        toolTipManager.tipHeightOfset= tipHeightOfset;
        toolTipManager.tipWidthOfset = tipWidthOfset;
        ToolTipManager.OnMouseHover (toolTipText , Input.mousePosition);
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(waitTime);
        
        ShowMesage();
    }

}
