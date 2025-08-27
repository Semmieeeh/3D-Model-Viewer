using UnityEngine;
using UnityEditor.Animations;

public class Besturings_Paneel : MonoBehaviour
{
    public Animator Animator;
    public bool hover;
    void Start()
    {
        
    }

    
    void Update()
    {
        Animator.SetBool("Hover", hover);
    }

    public void HoverOn () {
        hover= true;
    }
    public void HoverOff () {
        hover= false;
    }
}
