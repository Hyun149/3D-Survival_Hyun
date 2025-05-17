using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 조사 가능한 오브젝트 정보를 담는 데이터 클래스
/// ScriptableObject 형태로 인스펙터에서 데이터 생성 및 관리 가능
/// </summary>
[CreateAssetMenu(menuName = "Game/Inspectable Data")]
public class InspectableData : ScriptableObject
{
    public string objectName;
    [TextArea] public string description;
}
