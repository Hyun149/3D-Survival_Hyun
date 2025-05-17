using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 낙하 속도에 따라 데미지를 계산해 체력에 반영하는 컴포넌트
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
    /// 낙하 중 속도를 기록하고, 착지 시 데미지를 계산
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
