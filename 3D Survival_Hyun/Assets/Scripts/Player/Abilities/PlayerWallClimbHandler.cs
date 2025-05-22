using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ���� Ÿ�� ���� �� �ֵ��� ó���ϴ� ������Ʈ
/// Raycast�� ���� �����ϰ�, ���� ���� �Ͽ� Ŭ���̹� �� ���� ����
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerWallClimbHandler : MonoBehaviour
{
    [Header("�� Ÿ�� ����")]
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
    /// �� �����Ӹ��� �� ���� �� Ŭ���̹� ó��
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
    /// ���� �տ� �ΰ� �ִ��� �����ϴ� ���� (Ratcast ���)
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
    /// �� Ÿ�� ���۽�  ȣ��, �߷� ���� �� ������ȯ
    /// </summary>
    /// <param name="normal">������ ���� ����</param>
    private void StartClimb(Vector3 normal)
    {
        isClimbing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        wallNormal = normal;
    }

    /// <summary>
    /// ���� Ÿ�� �ö󰡴� ó�� �� ���� �Է� üũ
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
    /// �� Ÿ�� ���� �� ȣ��, �߷� ���� �� ���� ����
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
