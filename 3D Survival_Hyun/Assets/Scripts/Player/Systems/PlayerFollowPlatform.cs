using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ �����̴� �÷��� ���� ���� ��
/// �÷����� �̵�����ŭ �÷��̾ �Բ� �̵���Ű�� ������Ʈ
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerFollowPlatform : MonoBehaviour
{
    private Rigidbody playerRb;
    private MovingPlatform currentPlatform;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// �� �����Ӹ��� �÷����� ���� ���̶�� �÷����� �̵����� �÷��̾ ����
    /// </summary>
    /// <param name="collision">�浹 ����</param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (currentPlatform == null)
            {
                currentPlatform = collision.gameObject.GetComponent<MovingPlatform>();
            }

            if (currentPlatform != null)
            {
                playerRb.position += currentPlatform.DeltaPosition;
            }
        }
    }

    /// <summary>
    /// �÷������� �浹�� ����Ǹ� ���� ����
    /// </summary>
    /// <param name="collision">�浹 ����</param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
    }
}
