using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public class Viewer_Button_Swap : MonoBehaviour
{
    public GameObject buttonImage;
    public Animation uIElementAni;
    public Sprite open;
    public AnimationClip openAni;
    public Sprite close;
    public AnimationClip closeAni;
    private Image curImage;
    public bool uIActive;

    // Start voor het op vragen van de image waar de sprite in aangepased moet worden.
    void Start()
    {
        curImage = buttonImage.GetComponent<Image>();
        uIActive = false;
    }

    // Het wisselen van de functie en sprite van de knop.
    // Start ook de aniematie voor het openen/suiten van het menu.
    public void ButtonSwap () {
        if (uIActive == true) {
            uIElementAni.clip = closeAni;
            uIElementAni.Play();
            curImage.sprite = open;
            uIActive = false;
        }
        else if (uIActive == false) {
            uIElementAni.clip = openAni;
            uIElementAni.Play();
            curImage.sprite = close;
            uIActive = true;
        }       
    }
}
