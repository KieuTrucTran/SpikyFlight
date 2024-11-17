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
        if(GetComponent<Bird>().isDead == false)
        {
            animator.Play("Jump");
        } else
        {
            animator.Play("Dead");
        };
        
    }

    public void PlayDeadAnimation()
    {
        animator.Play("Dead");
    }
}
