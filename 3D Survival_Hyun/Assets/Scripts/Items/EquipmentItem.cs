using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장착 가능한 아이템의 기본 클래스 
/// 능력치 보정 기능을 포함하며, 상속을 통해 고유 효과 구현 가능
/// </summary>
public class EquipmentItem : MonoBehaviour
{
    public string itemName = "Unnamed Equipment";

    [Header("능력치 보정치")]
    public float speedBonus;
    public float jumpPowerBonus;
    public float fallDamageReductionBonus;

    /// <summary>
    /// 아이템이 장착 될 때 호출되는 가상 메서드
    /// 상속 클래스에서 오버라이드하여 고유 효과를 구현함
    /// </summary>
    public virtual void OnEquip()
    {
        // 장비 장착 시 실행할 기본 로직 (기본은 비어 있음)
    }
}
