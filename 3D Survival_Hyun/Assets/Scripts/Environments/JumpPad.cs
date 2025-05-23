using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 닿으면 위로 튕겨주는 점프대 오브젝트
/// 지정된 힘으로 순간적인 점프 효과를 부여함
/// </summary>
public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    /// <summary>
    /// 플레이어가 충돌했을 때 위 방향으로 힘을 가해 점프시키는 로직
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null && collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
