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
    void Start()
    {
        curImage = buttonImage.GetComponent<Image>();
        uIActive = false;
    }
    void Update()
    {
        
    }

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

    public void ButtonActif () {
        //buttonImage.GetComponent<Button>().enabled = true;
    }
    public void ButtonInActif () {
        //buttonImage.GetComponent<Button> ().enabled = false;
    }
}
