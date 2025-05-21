using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장착 가능한 아이템의 기본 클래스
/// </summary>
public class EquipmentItem : MonoBehaviour
{
    public string itemName = "Unnamed Equipment";

    public virtual void OnEquip()
    {
        
    }
}
