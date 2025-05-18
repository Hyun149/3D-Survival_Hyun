using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 아이템의 정보를 정의하는 데이터 클래스
/// ScriptableObject 형태로 생성하여 이름, 설명, 아이콘, 효과값 등을 관리함
/// </summary>
[CreateAssetMenu(menuName ="Game/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;

    [Header("소비 아이템 전용 옵션")]
    public float healAmount;     // 체력 회복량
    public float jumpBoost;      // (선택) 점프력 증가
    public float duration;       // (선택) 버프 지속 시간
}
