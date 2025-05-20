using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 애니메이션 상태를 제어하는 전용 클래스
/// 이동, 점프 등의 트리거 또는 상태 변화를 Animator에 전달함
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    /// <summary>
    /// 점프 트리거 애니메이션 실행
    /// </summary>
    public void PlayJump()
    {
        animator.SetTrigger("Jump");
    }

    /// <summary>
    /// 이동 중 여부에 따라 달리기 애니메이션 활성화
    /// </summary>
    /// <param name="isRunning"></param>
    public void SetRunning(bool isRunning)
    {
        animator.SetBool("Run Forward", isRunning);
    }
}
