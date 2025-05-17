using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 이동 및 점프 처리를 담당하는 컴포넌트
/// 입력값 기반으로 Rigidbody 이동 및 점프를 실행하며, 애니메이션 연동과
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;


    private Rigidbody rb;
    private PlayerInputHandler input;
    private GroundChecker groundChecker;
    private PlayerAnimator playerAnimator;

    /// <summary>
    /// 관련 컴포넌트 초기화
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInputHandler>();
        groundChecker = GetComponent<GroundChecker>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    /// <summary>
    /// 고정 주기마다 이동 및 점프 처리
    /// </summary>
    void FixedUpdate()
    {
        Move();

        if (input.JumpPressed && groundChecker.IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            playerAnimator?.PlayJump();
        }

        input.ClearInput();
    }

    /// <summary>
    /// 입력된 방향에 따라 Rigidbody 이동 및 달리기 애니메이션 트리거
    /// </summary>
    private void Move()
    {
        Vector3 direction = transform.forward * input.MovementInput.y + transform.right * input.MovementInput.x;
        direction *= moveSpeed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        if (playerAnimator != null)
        {
            bool isRunning = input.MovementInput.magnitude > 0.1f;
            playerAnimator.SetRunning(isRunning);
        }
    }

    /// <summary>
    /// 일정 시간동안 점프력을 증가시키는 버프 적용
    /// </summary>
    /// <param name="amount">추가 점프력</param>
    /// <param name="duration">버프 지속 시간 (초)</param>
    public void ApplyJumpBoost(float amount, float duration)
    {
        StartCoroutine(JumpBoostRoutine(amount, duration));
    }

    /// <summary>
    /// 점프력 버프 처리 코루틴
    /// </summary>
    private IEnumerator JumpBoostRoutine(float amount, float duration)
    {
        jumpPower += amount;
        yield return new WaitForSeconds(duration);
        jumpPower -= amount;
    }
}
