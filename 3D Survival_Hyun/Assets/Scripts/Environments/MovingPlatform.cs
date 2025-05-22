using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 두 지점(Point A, Point B)을 왕복하는 움직이는 플랫폼 클래스
/// Rigidbody 기반으로 이동하며, 외부에서 DeltaPosition 값을 참조 가능함
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;

    private Rigidbody rb;
    private bool movingToB = true;

    private Vector3 previousPosition;
    public Vector3 DeltaPosition { get; private set; }

    /// <summary>
    /// Rigidbody 및 초기 설정 적용
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    /// <summary>
    /// 물리연산 주기마다 플랫폼을 왕복 이동시킴
    /// DeltaPosition을 계산해 외부에서 상대 이동 처리 시 활용 가능
    /// </summary>
    void FixedUpdate()
    {
        Vector3 target = movingToB ? pointB.position : pointA.position;
        Vector3 newPosition = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        DeltaPosition = newPosition - rb.position;
        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, target) < 0.05f)
        {
            movingToB = !movingToB;
        }

        previousPosition = rb.position;
    }

    /// <summary>
    /// 에디터에서 선택 시 A-B 지점 간 선을 시각화함 (디버그용)
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
