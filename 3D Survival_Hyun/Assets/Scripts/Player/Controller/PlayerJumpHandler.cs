using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �⺻ ����, ���� ����, ���� ������ ���� �����ϴ� ������Ʈ
/// </summary>
public class PlayerJumpHandler : MonoBehaviour
{
    [Header("JumpSettings")]
    [SerializeField] private float jumpPower = 10f;

    [Header("Component References")]
    [SerializeField] private PlayerDoubleJump doubleJump;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerAnimator animator;

    /// <summary>
    /// ���� �Է��� ������ �� �����̸� �⺻ ����, �����̸� ���� ������ �õ�
    /// </summary>
    /// <param name="jumpPressed">���� Ű �Է� ����</param>
    public void TryJump(bool jumpPressed)
    {
        if (!jumpPressed) return;

        if (groundChecker.IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator?.PlayJump();
        }
        else
        {
            doubleJump.TryDoubleJump();
        }
    }

    /// <summary>
    /// ���� �ð� ���� �������� ������Ű�� ���� ������ ����
    /// </summary>
    /// <param name="amount">������ų ������</param>
    /// <param name="duration">���� ���� �ð�(��)</param>
    public void ApplyJumpBoost(float amount, float duration)
    {
        StartCoroutine(JumpBoostRoutine(amount, duration));
    }

    /// <summary>
    /// ���鿡 ����� �� ���� ���� ���� �ʱ�ȭ
    /// </summary>
    private void FixedUpdate()
    {
        if (groundChecker.IsGrounded())
        {
            doubleJump.ResetDoubleJump();
        }
    }

    /// <summary>
    /// ���� �ð����� JumpPower�� ������Ű�� ���� ó�� ��ƾ
    /// </summary>
    /// <param name="amount">�߰��� ������</param>
    /// <param name="duration">���� �ð�</param>
    /// <returns>�ڷ�ƾ</returns>
    private IEnumerator JumpBoostRoutine(float amount, float duration)
    {
        jumpPower += amount;
        yield return new WaitForSeconds(duration);
        jumpPower -= amount;
    }
 }
