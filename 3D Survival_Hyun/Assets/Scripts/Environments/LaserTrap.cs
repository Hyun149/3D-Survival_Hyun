using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 구간을 레이저로 감시하며, 플레이어가 통과하면 트랩을 발동합니다.
/// 시각적 레이저 효과는 LineRenderer를 사용하며, 감지는 주기적으로 Raycast로 수행됩니다.
/// </summary>
public class LaserTrap : MonoBehaviour
{
    [Header("레이저 설정")]
    [SerializeField] private Transform laserStart;
    [SerializeField] private Transform laserEnd;
    [SerializeField] private float detectionInterval;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("트랩 효과")]
    [SerializeField] private float damageAmount;

    private float timer;

    /// <summary>
    /// 컴포넌트 유효성 검사 및 초기화
    /// </summary>
    private void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    /// <summary>
    /// 매 프레임마다 감지 타이머를 갱신하고 일정 주기로 감지 수행
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= detectionInterval)
        {
            timer = 0f;
            ScanLaser();
        }

        UpdateLaserVisual();
    }

    /// <summary>
    /// 레이저 구간에 플레이어가 감지되면 데미지를 적용합니다.
    /// </summary>
    private void ScanLaser()
    {
        Vector3 direction = laserEnd.position - laserStart.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(laserStart.position, direction.normalized, out RaycastHit hit, distance, detectionMask))
        {

            if (hit.collider.CompareTag("Player"))
            {
                var health = hit.collider.GetComponentInParent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
                else
                {
                    Debug.LogWarning("[레이저] PlayerHealth 컴포넌트를 찾지 못했습니다.");
                }
            }
        }
    }

    /// <summary>
    /// LineRenderer를 사용하여 레이저의 시각적 위치를 갱신합니다.
    /// </summary>
    private void UpdateLaserVisual()
    {
        if (lineRenderer == null)
        {
            return;
        }

        lineRenderer.SetPosition(0, laserStart.position);
        lineRenderer.SetPosition(1, laserEnd.position);
    }
}
