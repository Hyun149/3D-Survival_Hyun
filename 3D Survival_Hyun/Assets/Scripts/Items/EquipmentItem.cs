using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ �������� �⺻ Ŭ���� (�߻����� ��� ��� ����)
/// �ٸ� ��� ������ Ŭ������ �� Ŭ������ ��ӹ޾� OnEquip�� �������̵���
/// </summary>
public class EquipmentItem : MonoBehaviour
{
    public string itemName = "Unnamed Equipment";

    /// <summary>
    /// �������� ���� �� �� ȣ��Ǵ� ���� �޼���
    /// ��� Ŭ�������� �������̵��Ͽ� ���� ȿ���� ������
    /// </summary>
    public virtual void OnEquip()
    {
        // ��� ���� �� ������ �⺻ ���� (�⺻�� ��� ����)
    }
}
