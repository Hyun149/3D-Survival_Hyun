using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 움직이는 플랫폼 위에 있을 때
/// 플랫폼의 이동량만큼 플레이어를 함께 이동시키는 컴포넌트
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerFollowPlatform : MonoBehaviour
{
    private Rigidbody playerRb;
    private MovingPlatform currentPlatform;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 매 프레임마다 플랫폼과 접촉 중이라면 플랫폼의 이동량을 플레이어에 적용
    /// </summary>
    /// <param name="collision">충돌 정보</param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (currentPlatform == null)
            {
                currentPlatform = collision.gameObject.GetComponent<MovingPlatform>();
            }

            if (currentPlatform != null)
            {
                playerRb.position += currentPlatform.DeltaPosition;
            }
        }
    }

    /// <summary>
    /// 플랫폼과의 충돌이 종료되면 참조 해제
    /// </summary>
    /// <param name="collision">충돌 정보</param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
    }
}
