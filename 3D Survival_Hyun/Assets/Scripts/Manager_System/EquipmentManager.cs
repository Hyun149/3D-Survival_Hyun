using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장착 가능한 장비를 관리하고 교체하는 시스템
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    private EquipmentItem currentEquipped;

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
