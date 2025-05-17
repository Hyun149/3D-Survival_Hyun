using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ���鿡 ����ִ��� �Ǻ��ϴ� ������Ʈ
/// �� �������� Ray�� ���� �ٴڰ��� ���� ���θ� Ȯ����
/// </summary>
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    /// <summary>
    /// �� �������� Raycast�� �߻��� ���鿡 ��Ҵ��� �˻�
    /// ����� ��� ������ true ��ȯ, �����̸� false
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}
