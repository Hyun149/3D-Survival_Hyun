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
    /// ���� �̸��� ���� ��ġ(Transform)�� ������
    /// </summary>
    [System.Serializable]
    public class EquipmentSlot
    {
        public string slotName;
        public Transform attachPoint;
    }

    [SerializeField] private EquipmentSlot[] equipmentSlots;

    private List<EquipmentItem> equipmentItems = new List<EquipmentItem>();

    /// <summary>
    /// ������ ���� �̸��� ��� �������� �ν��Ͻ�ȭ�Ͽ� �����մϴ�.
    /// ������ ���� �ɷ�ġ�� ������ ��Ĩ�ϴ�.
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

        EquipmentItem item = equippedObj.GetComponent<EquipmentItem>();
        if (item != null)
        {
            equipmentItems.Add(item);
            item.OnEquip();
        }       
    }

    /// <summary>
    /// ���� ������ �� �̵��ӵ� ����ġ
    /// </summary>
    public float TotalSpeedBonus => SumStat(e => e.speedBonus);

    /// <summary>
    /// ���� ������ �� ������ ����ġ
    /// </summary>
    public float TotalJumpPowerBonus => SumStat(e => e.jumpPowerBonus);

    /// <summary>
    /// ���� ������ �� ���� ������ ������ (0~1)
    /// </summary>
    public float TotalFallDamageReductionBonus => SumStat(e => e.fallDamageReductionBonus);

    /// <summary>
    /// ��� ����Ʈ���� Ư�� �ɷ�ġ ����ġ�� �ջ��Ͽ� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="selector">EquipmentItem���� ������ �ɷ�ġ ���� �Լ�</param>
    /// <returns>���õ� �ɷ�ġ�� ����</returns>
    private float SumStat(System.Func<EquipmentItem, float> selector)
    {
        float sum = 0f;
        foreach(var item in equipmentItems)
        {
            if(item != null)
            {
                sum += selector(item);
            }
        }
        return sum;
    }
}
