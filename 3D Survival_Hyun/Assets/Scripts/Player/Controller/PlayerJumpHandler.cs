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
    /// 점프 입력이 들어왔을 때 지면이면 기본 점프, 공중이면 더블 점프를 시도
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
    /// 일정 시간 동안 점프력을 증가시키는 점프 버프를 적용
    /// </summary>
    /// <param name="amount">증가시킬 점프력</param>
    /// <param name="duration">버프 지속 시간(초)</param>
    public void ApplyJumpBoost(float amount, float duration)
    {
        StartCoroutine(JumpBoostRoutine(amount, duration));
    }

    /// <summary>
    /// 지면에 닿았을 때 더블 점프 상태 초기화
    /// </summary>
    private void FixedUpdate()
    {
        if (groundChecker.IsGrounded())
        {
            doubleJump.ResetDoubleJump();
        }
    }

    /// <summary>
    /// 일정 시간동안 JumpPower를 증가시키는 버프 처리 루틴
    /// </summary>
    /// <param name="amount">추가할 점프력</param>
    /// <param name="duration">지속 시간</param>
    /// <returns>코루틴</returns>
    private IEnumerator JumpBoostRoutine(float amount, float duration)
    {
        jumpPower += amount;
        yield return new WaitForSeconds(duration);
        jumpPower -= amount;
    }
 }
