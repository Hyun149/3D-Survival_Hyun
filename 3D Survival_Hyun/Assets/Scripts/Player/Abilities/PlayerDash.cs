using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� ��� ����� �����ϴ� Ŭ����
/// ���¹̳��� �Һ��Ͽ� ���� �ӵ��� �̵��ϸ�, ��Ÿ���� �����
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
    /// ������Ʈ �ʱ�ȭ �� Rigidbody �� StaminaSystem ���� ����
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        staminaSystem = GetComponent<StaminaSystem>();
    }

    /// <summary>
    /// �־��� �������� ��ø� �õ��մϴ�.
    /// ���¹̳��� ����ϰ� ��Ÿ���� ���� ��쿡�� ����˴ϴ�.
    /// </summary>
    /// <param name="direction">����� ���� ����</param>
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
    /// ��� �� ��Ÿ���� ó���ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
