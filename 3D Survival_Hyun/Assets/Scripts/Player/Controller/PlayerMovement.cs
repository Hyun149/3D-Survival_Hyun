using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 이동, 점프, 대시 처리를 담당하는 메인 모션 제어 컴포넌트
/// 입력 처리, 이동 방향 계산, 애니메이션 연동까지 포함
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float basemoveSpeed;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerJumpHandler jumpHandler;
    [SerializeField] private PlayerDash playerDash;


    private Rigidbody rb;
    private GroundChecker groundChecker;
    private PlayerAnimator playerAnimator;
    private EquipmentHandler equipmentHandler;

    private bool isDashing = false;

    /// <summary>
    /// 컴포넌트 참조 초기화 (GetComponent 또는 인스펙터 설정)
    /// </summary>
    private void Awake()
    {
        if (jumpHandler == null) jumpHandler = GetComponent<PlayerJumpHandler>();
        if (playerDash == null) playerDash = GetComponent<PlayerDash>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (groundChecker == null) groundChecker = GetComponent<GroundChecker>();
        if (playerAnimator == null) playerAnimator = GetComponent<PlayerAnimator>();
        if (inputHandler == null) inputHandler = GetComponent<PlayerInputHandler>();
        equipmentHandler = GetComponent<EquipmentHandler>();
    }

    /// <summary>
    /// 고정 프레임 주기마다 이동, 점프, 대시 처리 실행
    /// </summary>
    void FixedUpdate()
    {
        Move();
        jumpHandler.TryJump(inputHandler.JumpPressed);
        HandleDash();
        inputHandler.ClearInput();
    }

    /// <summary>
    /// 대시 입력이 들어왔을 경우 대시 실행
    /// </summary>
    private void HandleDash()
    {
        if (!inputHandler.DashPressed) return;

        Vector3 directtion = GetMoveDirection();
        playerDash.TryDash(directtion);
    }

    /// <summary>
    /// 현재 입력 방향을 기준으로 이동 방향 벡터 계산
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMoveDirection()
    {
        return transform.forward * inputHandler.MovementInput.y + transform.right * inputHandler.MovementInput.x;
    }

    /// <summary>
    /// 입력된 방향에 따라 Rigidbody 이동 및 달리기 애니메이션 트리거
    /// </summary>
    private void Move()
    {
        if (isDashing) return;

        float speedBonus = equipmentHandler?.TotalSpeedBonus ?? 0f;
        float finalSpeed = basemoveSpeed + speedBonus;

        Vector3 direction = GetMoveDirection() * finalSpeed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        if (playerAnimator != null)
        {
            bool isRunning = inputHandler.MovementInput.magnitude > 0.1f && !isDashing;

            playerAnimator.SetRunning(isRunning);
        }
    }

    /// <summary>
    /// 대시 시작 시 호출되며 일정 시간동안 IsDashing을 true로 유지
    /// </summary>
    public void NotifyDashStart()
    {
        isDashing = true;
        StartCoroutine(EndDashAfter(0.2f));
    }

    /// <summary>
    /// 지정된 시간 이후 isDashing을 false로 설정
    /// </summary>
    /// <param name="time">대시 지속 시간</param>
    /// <returns></returns>
    private IEnumerator EndDashAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isDashing = false;
    }
}
