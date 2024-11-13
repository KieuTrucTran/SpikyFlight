using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    } 

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayJumpAnimation();
        };
    }

    void PlayFlyAnimation()
    {
        animator.Play("Fly");
    }

    void PlayJumpAnimation()
    {
        animator.Play("Jump");
    }

    public void PlayDeadAnimation()
    {
        animator.Play("Dead");
    }
}
