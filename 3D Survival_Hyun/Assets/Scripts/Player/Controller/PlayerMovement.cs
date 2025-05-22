using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �̵�, ����, ��� ó���� ����ϴ� ���� ��� ���� ������Ʈ
/// �Է� ó��, �̵� ���� ���, �ִϸ��̼� �������� ����
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
    /// ������Ʈ ���� �ʱ�ȭ (GetComponent �Ǵ� �ν����� ����)
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
    /// ���� ������ �ֱ⸶�� �̵�, ����, ��� ó�� ����
    /// </summary>
    void FixedUpdate()
    {
        Move();
        jumpHandler.TryJump(inputHandler.JumpPressed);
        HandleDash();
        inputHandler.ClearInput();
    }

    /// <summary>
    /// ��� �Է��� ������ ��� ��� ����
    /// </summary>
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
    /// ��� ���� �� ȣ��Ǹ� ���� �ð����� IsDashing�� true�� ����
    /// </summary>
    public void NotifyDashStart()
    {
        isDashing = true;
        StartCoroutine(EndDashAfter(0.2f));
    }

    /// <summary>
    /// ������ �ð� ���� isDashing�� false�� ����
    /// </summary>
    /// <param name="time">��� ���� �ð�</param>
    /// <returns></returns>
    private IEnumerator EndDashAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isDashing = false;
    }
}
