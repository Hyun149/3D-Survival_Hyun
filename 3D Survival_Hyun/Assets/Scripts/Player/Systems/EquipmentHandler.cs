using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ��� �����ϴ� ������Ʈ 
/// Ư�� ������ �������� �ν����ͷ� ������ ���� ó����
/// </summary>
public class EquipmentHandler : MonoBehaviour
{
    /// <summary>
    /// ���� ��� ���� ������ ��Ÿ���� ����ü
    /// </summary>
    [System.Serializable]
    public class EquipmentSlot
    {
        public string slotName;
        public Transform attachPoint;
    }

    [SerializeField] private EquipmentSlot[] equipmentSlots;

    /// <summary>
    /// ������ ���Կ� ��� �������� �����մϴ�.
    /// �ش� ���� �̸��� �ش��ϴ� ��ġ�� �������� �ڽ����� �ν��Ͻ�ȭ��
    /// </summary>
    /// <param name="equipmentPrefab">������ ��� ������</param>
    /// <param name="slotName">������ ���� �̸�</param>
    public void Equip(GameObject equipmentPrefab, string slotName)
    {
        EquipmentSlot slot = System.Array.Find(equipmentSlots, s => s.slotName == slotName);
        if (slot == null)
        {
            Debug.LogWarning($"���� '{slotName}'");
            return;
        }

        GameObject equippedObj = Instantiate(equipmentPrefab, slot.attachPoint);
        equippedObj.transform.localPosition = Vector3.zero;
        equippedObj.transform.localRotation = Quaternion.identity;
    }
}
