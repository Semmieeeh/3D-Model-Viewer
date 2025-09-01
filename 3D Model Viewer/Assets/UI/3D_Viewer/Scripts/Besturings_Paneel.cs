using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
public class Besturings_Paneel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 
    public Animator Animator;
    private bool hover;
    private float menuOpenValu;
    public float menuOpenSpeed;

    // Bij Start zetten we hover uit voor de zeker hijd dat hij dicht blijft.
    void Start()
    {
        hover = false;  
    }
    // Hier zetten we hover aan zodra een PointerEnter event aan geroepen word op de game object waar de code zit.
    public void OnPointerEnter (PointerEventData eventData) {
        hover = true;
    }
    // Hier zetten we hover uit zodra een PointerExit event aan geroepen word op de game object waar de code zit.
    public void OnPointerExit(PointerEventData eventData) {
        hover = false;
    }
    // In de Update als de "Hover" op niet waar staat doen we de open warde naar benenden en als de "Hover" op waar staat doen we de waarde naar boven.
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
