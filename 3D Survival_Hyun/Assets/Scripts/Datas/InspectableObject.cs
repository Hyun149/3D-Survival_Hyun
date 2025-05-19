using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 조사 가능한 오브젝트에 부착되는 컴포넌트
/// 연결된 InspectableData를 통해 이름/설명 등의 정보를 제공함
/// </summary>
public class InspectableObject : MonoBehaviour
{
    public InspectableData data;
}
