using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerWallClimbHandler : MonoBehaviour
{
    [Header("벽 타기 설정")]
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody rb;
    private bool isClimbing = false;
    private Vector3 wallNormal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckWall();

        if (isClimbing)
        {
            HandleClimb();
        }
    }

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

    private void StartClimb(Vector3 normal)
    {
        isClimbing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        wallNormal = normal;
    }

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

    private void StopClimb()
    {
        if (isClimbing)
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }
}
