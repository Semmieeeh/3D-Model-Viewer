using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoldHandler : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public System.Action OnHoldButton;
    public System.Action OnReleaseButton;
    private bool _isHeld;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHeld = true;
        OnHoldButton?.Invoke();
        Debug.Log($"{gameObject.name} + pressed Holding");

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _isHeld = false;
        OnReleaseButton?.Invoke();
        Debug.Log($"{gameObject.name} + pressed Released");
    }
    void Update()
    {
        if( _isHeld)
        {
            OnHoldButton?.Invoke();
        }
    }
}
