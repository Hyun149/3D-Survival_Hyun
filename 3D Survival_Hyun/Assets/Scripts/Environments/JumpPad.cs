using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ������ ���� ƨ���ִ� ������ ������Ʈ
/// ������ ������ �������� ���� ȿ���� �ο���
/// </summary>
public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    /// <summary>
    /// �÷��̾ �浹���� �� �� �������� ���� ���� ������Ű�� ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null && collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
