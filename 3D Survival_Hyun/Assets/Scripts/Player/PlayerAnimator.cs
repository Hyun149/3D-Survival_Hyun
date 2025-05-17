using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� �ִϸ��̼� ���¸� �����ϴ� ���� Ŭ����
/// �̵�, ���� ���� Ʈ���� �Ǵ� ���� ��ȭ�� Animator�� ������
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    /// <summary>
    /// ���� Ʈ���� �ִϸ��̼� ����
    /// </summary>
    public void PlayJump()
    {
        animator.SetTrigger("Jump");
    }

    /// <summary>
    /// �̵� �� ���ο� ���� �޸��� �ִϸ��̼� Ȱ��ȭ
    /// </summary>
    /// <param name="isRunning"></param>
    public void SetRunning(bool isRunning)
    {
        animator.SetBool("Run Forward", isRunning);
    }
}
