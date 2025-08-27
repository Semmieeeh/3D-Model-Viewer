using UnityEngine;
using UnityEditor.Animations;

public class Besturings_Paneel : MonoBehaviour
{
    public Animator Animator;
    public bool hover;
    public float lerpValuMin = 0f;
    public float lerpValuMax = 1f;
    public float lerpValu;
    void Start()
    {
        
    }

    
    void Update()
    {
        Animator.SetBool("Hover", hover);
        Animator.SetFloat ("OpenVal" , lerpValu);
    }

    public void HoverOn () {
        lerpValu = Mathf.Lerp (lerpValuMin , lerpValuMax , 0.1f);
    }
    public void HoverOff () {
        lerpValu = Mathf.Lerp (lerpValuMax , lerpValuMin , 0.1f);
    }
}
