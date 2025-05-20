using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 지면에 닿아있는지 판별하는 컴포넌트
/// 네 방향으로 Ray를 쏴서 바닥과의 접촉 여부를 확인함
/// </summary>
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    /// <summary>
    /// 네 방향으로 Raycast를 발사해 지면에 닿았는지 검사
    /// 지면과 닿아 있으면 true 반환, 공중이면 false
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}
