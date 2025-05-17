using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트가 항상 카메라를 바라보도록 회전시키는 빌보드 처리 컴포넌트
/// 주로 월드 스페이스 UI, 체력바, 이름표 등에 사용
/// </summary>
public class BillBoard : MonoBehaviour
{
    private Transform cam;

    /// <summary>
    /// 메인 카메라 Transform을 캐싱
    /// </summary>
    void Start()
    {
        cam = Camera.main.transform;
    }

    /// <summary>
    /// 프레임 끝에서 오브젝트가 카메라 방향을 향하도록 회전
    /// </summary>
    void LateUpdate()
    {
        transform.forward = cam.forward;
    }
}
