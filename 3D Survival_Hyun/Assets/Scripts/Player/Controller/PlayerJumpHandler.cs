using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 기본 점프, 더블 점프, 점프 버프를 통합 관리하는 컴포넌트
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
    /// 점프 입력을 받아 점프 또는 이중 점프 실행
    /// </summary>
    /// <param name="jumpPressed">점프 키 입력 여부</param>
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
    /// 점프력 버프를 일정 시간 적용
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="duration"></param>
    public void ApplyJumpBoost(float amount, float duration)
    {
        StartCoroutine(JumpBoostRoutine(amount, duration));
    }

    private void FixedUpdate()
    {
        if (groundChecker.IsGrounded())
        {
            doubleJump.ResetDoubleJump();
        }
    }

    private IEnumerator JumpBoostRoutine(float amount, float duration)
    {
        jumpPower += amount;
        yield return new WaitForSeconds(duration);
        jumpPower -= amount;
    }
 }
