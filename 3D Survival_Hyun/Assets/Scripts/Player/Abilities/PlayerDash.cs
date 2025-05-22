using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 대시 기능을 제어하는 클래스
/// 스태미나를 소비하여 빠른 속도로 이동하며, 쿨타임이 적용됨
/// </summary>
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashStaminaCost;
    [SerializeField] private StaminaSystem staminaSystem;

    private Rigidbody rb;
    private bool canDash = true;

    /// <summary>
    /// 컴포넌트 초기화 시 Rigidbody 및 StaminaSystem 참조 설정
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        staminaSystem = GetComponent<StaminaSystem>();
    }

    /// <summary>
    /// 주어진 방향으로 대시를 시도합니다.
    /// 스태미나가 충분하고 쿨타임이 지난 경우에만 실행됩니다.
    /// </summary>
    /// <param name="direction">대시할 방향 벡터</param>
    public void TryDash(Vector3 direction)
    {
        if (!canDash)
        {
            return;
        }
        if (!staminaSystem.Consume(dashStaminaCost))
        {
            return;
        }

        rb.velocity = direction.normalized * dashForce;
        GetComponent<PlayerMovement>()?.NotifyDashStart();
        StartCoroutine(DashCooldown());
    }

    /// <summary>
    /// 대시 후 쿨타임을 처리하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
