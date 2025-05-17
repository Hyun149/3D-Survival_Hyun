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
    [TextArea] public string description;
    public ItemType itemType;

    public Sprite icon;
    public int Value;
}
