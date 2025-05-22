using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ �������� �����ϸ�, �÷��̾ ����ϸ� Ʈ���� �ߵ��մϴ�.
/// </summary>
public class LaserTrap : MonoBehaviour
{
    [Header("������ ����")]
    [SerializeField] private Transform laserStart;
    [SerializeField] private Transform laserEnd;
    [SerializeField] private float detectionInterval = 0.2f;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Ʈ�� ȿ��")]
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
                    Debug.LogWarning("[������] PlayerHealth ������Ʈ�� ã�� ���߽��ϴ�.");
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
