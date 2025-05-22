using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장착 가능한 아이템의 기본 클래스 (추상적인 장비 기능 제공)
/// 다른 장비 아이템 클래스는 이 클래스를 상속받아 OnEquip를 오버라이드함
/// </summary>
public class EquipmentItem : MonoBehaviour
{
    public string itemName = "Unnamed Equipment";

    /// <summary>
    /// 아이템이 장착 될 때 호출되는 가상 메서드
    /// 상속 클래스에서 오버라이드하여 고유 효과를 구현함
    /// </summary>
    public virtual void OnEquip()
    {
        // 장비 장착 시 실행할 기본 로직 (기본은 비어 있음)
    }
}
