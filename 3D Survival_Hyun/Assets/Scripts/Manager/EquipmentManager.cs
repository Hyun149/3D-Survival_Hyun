using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장착 가능한 장비를 관리하고 교체하는 시스템
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    private EquipmentItem currentEquipped;

    /// <summary>
    /// 새 장비 아이템을 장착하고, 기존 장비는 제거합니다.
    /// </summary>
    /// <param name="newItem"></param>
    public void Equip(EquipmentItem newItem)
    {
        if (currentEquipped != null)
        {
            Destroy(currentEquipped.gameObject);
        }

        currentEquipped = Instantiate(newItem, transform);
        currentEquipped.OnEquip();
    }
}
