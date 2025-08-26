using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public class Viewer_Button_Swap : MonoBehaviour
{
    public GameObject uIElement;
    public Animation uIElementAni;
    public Sprite open;
    public AnimationClip openAni;
    public Sprite close;
    public AnimationClip closeAni;
    private Image curImage;
    void Start()
    {
        curImage= GetComponent<Image>();
    }
    void Update()
    {
        
    }

    public void ButtonSwap () {
        if (uIElement.activeSelf) {
            uIElementAni.clip = closeAni;
            uIElementAni.Play();
            curImage.sprite = open;
            Debug.Log ("Uit");
        }
        else {
            uIElementAni.clip = openAni;
            uIElement.SetActive(true);
            curImage.sprite = close;
            Debug.Log ("Aan");
        }

        
    }
    public void Close () {

        uIElement.SetActive(false);
    }
}
