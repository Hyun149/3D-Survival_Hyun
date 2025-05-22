using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ���� �ӵ��� ���� �������� ����� ü�¿� �ݿ��ϴ� ������Ʈ
/// ���� �Ӱ�ġ �̻��� ���� �ӵ��� �����ϸ� �������� ����
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerFallDamage : MonoBehaviour
{
    [SerializeField] private float speedThreshold;
    [SerializeField] private float damageMultiplier;

    private float previousYVelocity = 0f;
    private Rigidbody rb;
    private PlayerHealth playerHealth;
    private GroundChecker groundChecker;
    private EquipmentHandler equipmentHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        groundChecker = GetComponent<GroundChecker>();
        equipmentHandler = GetComponent<EquipmentHandler>();
    }

    private void Update()
    {
        CheckFallDamage();
    }

    /// <summary>
    /// ���� ���鿡 ��Ҵ��� Ȯ���ϰ�, ���� �����ӿ����� ���� �ӵ��� �������� ���� �������� ������
    /// ���� ��� ���� ��� �������� ���ҽ�Ŵ
    /// </summary>
    public void CheckFallDamage()
    {
        if (groundChecker.IsGrounded())
        {
            if (previousYVelocity < speedThreshold)
            {
                float rawDamage = Mathf.Abs(previousYVelocity) * damageMultiplier;

                float reductionRatio = Mathf.Clamp01(equipmentHandler?.TotalFallDamageReductionBonus ?? 0f);
                float finalDamage = rawDamage * (1f - reductionRatio);

                playerHealth?.TakeDamage(finalDamage);
            }

            previousYVelocity = 0f;
        }
        else
        {
            previousYVelocity = rb.velocity.y;
        }
    }
}
