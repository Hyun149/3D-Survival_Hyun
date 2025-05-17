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
    [SerializeField] private float fallDamageThreshold = -15f;

    private float previousYvelocity = 0f;
    private Rigidbody rb;
    private PlayerHealth playerHealth;
    private GroundChecker groundChecker;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        groundChecker = GetComponent<GroundChecker>();
    }

    /// <summary>
    /// 현재 지면에 닿았는지 확인하고, 이전 프레임에서의 낙하 속도를 기준으로 낙하 데미지를 적용함
    /// </summary>
    public void CheckFallDamage()
    {
        if (groundChecker.IsGrounded())
        {
            if (previousYvelocity < fallDamageThreshold)
            {
                float damage = Mathf.Abs(previousYvelocity) * 2f;
                playerHealth?.TakeDamage(damage);
            }

            previousYvelocity = 0f;
        }
        else
        {
            previousYvelocity = rb.velocity.y;
        }
    }
}
