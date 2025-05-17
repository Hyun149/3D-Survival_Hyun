using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ ������Ʈ ������ ��� ������ Ŭ����
/// ScriptableObject ���·� �ν����Ϳ��� ������ ���� �� ���� ����
/// </summary>
[CreateAssetMenu(menuName = "Game/Inspectable Data")]
public class InspectableData : ScriptableObject
{
    public string objectName;
    [TextArea] public string description;
}
