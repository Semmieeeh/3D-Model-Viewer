using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonHoldHandler : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public System.Action OnHoldButton;
    public System.Action OnReleaseButton;
    private bool _isHeld;
    [SerializeField] private Animation _rotateArrowAnim;


   
       
    public void OnPointerDown(PointerEventData eventData)
    {
        _isHeld = true;
        OnHoldButton?.Invoke();
   
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _isHeld = false;
        OnReleaseButton?.Invoke();
        
    }
    void Update()
    {
        if (_isHeld)
        {
            OnHoldButton?.Invoke();
        }
    }
}