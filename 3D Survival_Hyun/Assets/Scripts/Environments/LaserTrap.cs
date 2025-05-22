using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 구간을 레이저로 감시하며, 플레이어가 통과하면 트랩을 발동합니다.
/// </summary>
public class LaserTrap : MonoBehaviour
{
    [Header("레이저 설정")]
    [SerializeField] private Transform laserStart;
    [SerializeField] private Transform laserEnd;
    [SerializeField] private float detectionInterval = 0.2f;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("트랩 효과")]
    [SerializeField] private float damageAmount = 20f;

    private float timer;

    private void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

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
