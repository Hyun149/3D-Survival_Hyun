using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어에 장비를 장착하는 컴포넌트. 특정 부위에 프리팹을 인스펙터로 연결해 장착 처리
/// </summary>
public class EquipmentHandler : MonoBehaviour
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public string slotName;
        public Transform attachPoint;
    }

    [SerializeField] private EquipmentSlot[] equipmentSlots;

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
