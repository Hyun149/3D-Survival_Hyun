using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 이동 및 대시 처리를 담당하는 컴포넌트
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerJumpHandler jumpHandler;
    [SerializeField] private PlayerDash playerDash;


    private Rigidbody rb;
    private GroundChecker groundChecker;
    private PlayerAnimator playerAnimator;
    private bool isDashing = false;

    private void Awake()
    {
        if (jumpHandler == null) jumpHandler = GetComponent<PlayerJumpHandler>();
        if (playerDash == null) playerDash = GetComponent<PlayerDash>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (groundChecker == null) groundChecker = GetComponent<GroundChecker>();
        if (playerAnimator == null) playerAnimator = GetComponent<PlayerAnimator>();
        if (inputHandler == null) inputHandler = GetComponent<PlayerInputHandler>();
    }

    /// <summary>
    /// 고정 주기마다 이동 및 점프 처리
    /// </summary>
    void FixedUpdate()
    {
        Move();
        jumpHandler.TryJump(inputHandler.JumpPressed);
        HandleDash();
        inputHandler.ClearInput();
    }

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

        Vector3 direction = GetMoveDirection() * moveSpeed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        if (playerAnimator != null)
        {
            bool isRunning = inputHandler.MovementInput.magnitude > 0.1f && !isDashing;

            playerAnimator.SetRunning(isRunning);
        }
    }

    public void NotifyDashStart()
    {
        isDashing = true;
        StartCoroutine(EndDashAfter(0.2f));
    }

    private IEnumerator EndDashAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isDashing = false;
    }
}
