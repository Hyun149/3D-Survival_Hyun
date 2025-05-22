using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 벽을 타고 오를 수 있도록 처리하는 컴포넌트
/// Raycast로 벽을 감지하고, 일정 조건 하에 클라이밍 및 점프 가능
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerWallClimbHandler : MonoBehaviour
{
    [Header("벽 타기 설정")]
    [SerializeField] private float climbSpeed;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody rb;
    private bool isClimbing = false;
    private Vector3 wallNormal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 매 프레임마다 벽 감지 및 클라이밍 처리
    /// </summary>
    private void Update()
    {
        CheckWall();

        if (isClimbing)
        {
            HandleClimb();
        }
    }

    /// <summary>
    /// 벽을 앞에 두고 있는지 감지하는 로직 (Ratcast 기반)
    /// </summary>
    private void CheckWall()
    {
        Vector3 direction = transform.forward;
        Ray ray = new Ray(transform.position, direction);
        Debug.DrawRay(transform.position, direction * wallCheckDistance, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, wallCheckDistance, wallLayer))
        {
            if (!isClimbing)
            {
                StartClimb(hit.normal);
            }
        }
        else
        {
            StopClimb();
        }
    }

    /// <summary>
    /// 벽 타기 시작시  호출, 중력 제거 및 상태전환
    /// </summary>
    /// <param name="normal">감지된 벽의 법선</param>
    private void StartClimb(Vector3 normal)
    {
        isClimbing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        wallNormal = normal;
    }

    /// <summary>
    /// 벽을 타고 올라가는 처리 및 점프 입력 체크
    /// </summary>
    private void HandleClimb()
    {
        float vertical = Input.GetAxis("Vertical");
        Vector3 climbDirection = Vector3.up * vertical;
        rb.MovePosition(rb.position + climbDirection * climbSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Vector3 jumpDir = wallNormal + Vector3.up;
            rb.AddForce(jumpDir.normalized * 6f, ForceMode.Impulse);
            StopClimb();
        }
    }

    /// <summary>
    /// 벽 타기 종료 시 호출, 중력 복원 및 상태 리셋
    /// </summary>
    private void StopClimb()
    {
        if (isClimbing)
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }
}
