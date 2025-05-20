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
    /// ���� ���鿡 ��Ҵ��� Ȯ���ϰ�, ���� �����ӿ����� ���� �ӵ��� �������� ���� �������� ������
    /// </summary>
    public void CheckFallDamage()
    {
        if (groundChecker.IsGrounded())
        {
            if (previousYVelocity < speedThreshold)
            {
                float damage = Mathf.Abs(previousYVelocity) * damageMultiplier;
                playerHealth?.TakeDamage(damage);

                Debug.Log($"[FallDamage] ���� �ӵ� {previousYVelocity:F1} �� {damage:F1} ������");
            }

            if (previousYVelocity < 0)
            {
                Debug.Log($"[����] ���� ���� �ӵ�: {previousYVelocity:F2}");
            }

            previousYVelocity = 0f;
        }
        else
        {
            previousYVelocity = rb.velocity.y;
        }
    }
}
