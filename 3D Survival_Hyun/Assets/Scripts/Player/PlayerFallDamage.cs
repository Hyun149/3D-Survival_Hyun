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
    [SerializeField] private float speedThreshold = -12f;
    [SerializeField] private float damageMultiplier = 2f;

    private float previousYVelocity = 0f;
    private Rigidbody rb;
    private PlayerHealth playerHealth;
    private GroundChecker groundChecker;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        groundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        CheckFallDamage();
    }

    /// <summary>
    /// 현재 지면에 닿았는지 확인하고, 이전 프레임에서의 낙하 속도를 기준으로 낙하 데미지를 적용함
    /// </summary>
    public void CheckFallDamage()
    {
        if (groundChecker.IsGrounded())
        {
            if (previousYVelocity < speedThreshold)
            {
                float damage = Mathf.Abs(previousYVelocity) * damageMultiplier;
                playerHealth?.TakeDamage(damage);

                Debug.Log($"[FallDamage] 착지 속도 {previousYVelocity:F1} → {damage:F1} 데미지");
            }

            if (previousYVelocity < 0)
            {
                Debug.Log($"[착지] 이전 낙하 속도: {previousYVelocity:F2}");
            }

            previousYVelocity = 0f;
        }
        else
        {
            previousYVelocity = rb.velocity.y;
        }
    }
}
