using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ �������� �⺻ Ŭ���� 
/// �ɷ�ġ ���� ����� �����ϸ�, ����� ���� ���� ȿ�� ���� ����
/// </summary>
public class EquipmentItem : MonoBehaviour
{
    public string itemName = "Unnamed Equipment";

    [Header("�ɷ�ġ ����ġ")]
    public float speedBonus;
    public float jumpPowerBonus;
    public float fallDamageReductionBonus;

    /// <summary>
    /// �������� ���� �� �� ȣ��Ǵ� ���� �޼���
    /// ��� Ŭ�������� �������̵��Ͽ� ���� ȿ���� ������
    /// </summary>
    public virtual void OnEquip()
    {
        // ��� ���� �� ������ �⺻ ���� (�⺻�� ��� ����)
    }
}
