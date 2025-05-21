using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ ��� �����ϰ� ��ü�ϴ� �ý���
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
