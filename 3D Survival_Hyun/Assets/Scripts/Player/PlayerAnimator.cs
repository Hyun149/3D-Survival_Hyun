using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayJump()
    {
        animator.SetTrigger("Jump");
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("Run Forward", isRunning);
    }
}
