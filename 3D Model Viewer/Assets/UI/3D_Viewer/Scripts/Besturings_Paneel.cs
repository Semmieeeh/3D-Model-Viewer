using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class Besturings_Paneel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator Animator;
    private bool hover;
    private float menuOpenValu;
    public float menuOpenSpeed;
    void Start()
    {
        hover= false;  
    }

    public void OnPointerEnter (PointerEventData eventData) {
        hover= true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        hover= false;
    }

    void Update()
    {
        Animator.SetBool("Hover", hover);
        Animator.SetFloat ("OpenVal" , menuOpenValu);

        if(hover == false && menuOpenValu > 0 ) {
            menuOpenValu -= menuOpenSpeed * Time.deltaTime;
        }
        else if(hover == true && menuOpenValu < 10) {
            menuOpenValu += menuOpenSpeed * Time.deltaTime;
        }
    }
       
}
