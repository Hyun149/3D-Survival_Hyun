using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 낙하 속도에 따라 데미지를 계산해 체력에 반영하는 컴포넌트
/// 일정 임계치 이상의 낙하 속도로 착지하면 데미지를 입음
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
    /// 현재 지면에 닿았는지 확인하고, 이전 프레임에서의 낙하 속도를 기준으로 낙하 데미지를 적용함
    /// 감쇠 장비가 있을 경우 데미지를 감소시킴
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
