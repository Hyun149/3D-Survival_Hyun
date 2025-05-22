using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어에 장비를 장착하는 컴포넌트 
/// 특정 부위에 프리팹을 인스펙터로 연결해 장착 처리함
/// </summary>
public class EquipmentHandler : MonoBehaviour
{
    /// <summary>
    /// 개별 장비 슬롯 정보를 나타내는 구조체
    /// 슬롯 이름과 장착 위치(Transform)를 포함함
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
    /// 지정한 슬롯 이름에 장비 프리팹을 인스턴스화하여 장착합니다.
    /// 장착된 장비는 능력치에 영향을 미칩니다.
    /// </summary>
    /// <param name="equipmentPrefab">장착할 장비 프리팹</param>
    /// <param name="slotName">장착할 슬롯 이름</param>
    public void Equip(GameObject equipmentPrefab, string slotName)
    {
        EquipmentSlot slot = System.Array.Find(equipmentSlots, s => s.slotName == slotName);
        if (slot == null)
        {
            Debug.LogWarning($"슬롯 '{slotName}'");
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
    /// 현재 장비들의 총 이동속도 보정치
    /// </summary>
    public float TotalSpeedBonus => SumStat(e => e.speedBonus);

    /// <summary>
    /// 현재 장비들의 총 점프력 보정치
    /// </summary>
    public float TotalJumpPowerBonus => SumStat(e => e.jumpPowerBonus);

    /// <summary>
    /// 현재 장비들의 총 낙하 데미지 감소율 (0~1)
    /// </summary>
    public float TotalFallDamageReductionBonus => SumStat(e => e.fallDamageReductionBonus);

    /// <summary>
    /// 장비 리스트에서 특정 능력치 보정치를 합산하여 반환합니다.
    /// </summary>
    /// <param name="selector">EquipmentItem에서 추출할 능력치 선택 함수</param>
    /// <returns>선택된 능력치의 총합</returns>
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
