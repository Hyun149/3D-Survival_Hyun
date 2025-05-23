using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ �������� �����ϸ�, �÷��̾ ����ϸ� Ʈ���� �ߵ��մϴ�.
/// �ð��� ������ ȿ���� LineRenderer�� ����ϸ�, ������ �ֱ������� Raycast�� ����˴ϴ�.
/// </summary>
public class LaserTrap : MonoBehaviour
{
    [Header("������ ����")]
    [SerializeField] private Transform laserStart;
    [SerializeField] private Transform laserEnd;
    [SerializeField] private float detectionInterval;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Ʈ�� ȿ��")]
    [SerializeField] private float damageAmount;

    private float timer;

    /// <summary>
    /// ������Ʈ ��ȿ�� �˻� �� �ʱ�ȭ
    /// </summary>
    private void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    /// <summary>
    /// �� �����Ӹ��� ���� Ÿ�̸Ӹ� �����ϰ� ���� �ֱ�� ���� ����
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
    /// ������ ������ �÷��̾ �����Ǹ� �������� �����մϴ�.
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
                    Debug.LogWarning("[������] PlayerHealth ������Ʈ�� ã�� ���߽��ϴ�.");
                }
            }
        }
    }

    /// <summary>
    /// LineRenderer�� ����Ͽ� �������� �ð��� ��ġ�� �����մϴ�.
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
