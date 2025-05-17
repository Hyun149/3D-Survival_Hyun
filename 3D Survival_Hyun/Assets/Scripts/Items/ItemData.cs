using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �������� ������ �����ϴ� ������ Ŭ����
/// ScriptableObject ���·� �����Ͽ� �̸�, ����, ������, ȿ���� ���� ������
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
