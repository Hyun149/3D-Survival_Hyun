using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʈ�� �׻� ī�޶� �ٶ󺸵��� ȸ����Ű�� ������ ó�� ������Ʈ
/// �ַ� ���� �����̽� UI, ü�¹�, �̸�ǥ � ���
/// </summary>
public class BillBoard : MonoBehaviour
{
    private Transform cam;

    /// <summary>
    /// ���� ī�޶� Transform�� ĳ��
    /// </summary>
    void Start()
    {
        cam = Camera.main.transform;
    }

    /// <summary>
    /// ������ ������ ������Ʈ�� ī�޶� ������ ���ϵ��� ȸ��
    /// </summary>
    void LateUpdate()
    {
        transform.forward = cam.forward;
    }
}
