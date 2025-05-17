using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ���� �ӵ��� ���� �������� ����� ü�¿� �ݿ��ϴ� ������Ʈ
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
    /// ���� �� �ӵ��� ����ϰ�, ���� �� �������� ���
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
