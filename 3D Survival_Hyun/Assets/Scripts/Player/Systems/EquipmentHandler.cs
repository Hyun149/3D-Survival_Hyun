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
    /// </summary>
    [System.Serializable]
    public class EquipmentSlot
    {
        public string slotName;
        public Transform attachPoint;
    }

    [SerializeField] private EquipmentSlot[] equipmentSlots;

    /// <summary>
    /// 지정한 스롯에 장비 프리팹을 장착합니다.
    /// 해당 슬롯 이름에 해당하는 위치에 프리팹을 자식으로 인스턴스화함
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
    }
}
