using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �⺻ ����, ���� ����, ���� ������ ���� �����ϴ� ������Ʈ
/// ��� ������ �Ͻ��� ������ �����Ͽ� �������� �����
/// </summary>
public class PlayerJumpHandler : MonoBehaviour
{
    [Header("JumpSettings")]
    [SerializeField] private float baseJumpPower = 10f;

    [Header("Component References")]
    [SerializeField] private PlayerDoubleJump doubleJump;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerAnimator animator;

    private EquipmentHandler equipmentHandler;
    private float jumpPowerBonusFromBuff = 0f;

    private void Awake()
    {
        if (doubleJump == null) doubleJump = GetComponent<PlayerDoubleJump>();
        if (groundChecker == null) groundChecker = GetComponent<GroundChecker>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponent<PlayerAnimator>();

        equipmentHandler = GetComponent<EquipmentHandler>();
    }

    /// <summary>
    /// ���� �Է��� ������ �� �����̸� �⺻ ����, �����̸� ���� ������ �õ��մϴ�.
    /// ��� �� ������ ���� ������ ���Ե˴ϴ�.
    /// </summary>
    /// <param name="jumpPressed">���� Ű �Է� ����</param>
    public void TryJump(bool jumpPressed)
    {
        if (!jumpPressed) return;

        if (groundChecker.IsGrounded())
        {
            float equipmentBonus = equipmentHandler?.TotalJumpPowerBonus ?? 0f;
            float totalJumpPower = baseJumpPower + jumpPowerBonusFromBuff + equipmentBonus;

            rb.AddForce(Vector3.up * totalJumpPower, ForceMode.Impulse);
            animator?.PlayJump();

            Debug.Log($"[����] ������: {totalJumpPower} (�⺻ {baseJumpPower} + ��� {equipmentBonus} + ���� {jumpPowerBonusFromBuff})");
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
        jumpPowerBonusFromBuff += amount;
        yield return new WaitForSeconds(duration);
        jumpPowerBonusFromBuff -= amount;
    }
 }
