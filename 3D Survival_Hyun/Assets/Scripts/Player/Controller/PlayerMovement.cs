using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �̵� �� ��� ó���� ����ϴ� ������Ʈ
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
    /// ���� �ֱ⸶�� �̵� �� ���� ó��
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
    /// ���� �Է� ������ �������� �̵� ���� ���� ���
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMoveDirection()
    {
        return transform.forward * inputHandler.MovementInput.y + transform.right * inputHandler.MovementInput.x;
    }

    /// <summary>
    /// �Էµ� ���⿡ ���� Rigidbody �̵� �� �޸��� �ִϸ��̼� Ʈ����
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
