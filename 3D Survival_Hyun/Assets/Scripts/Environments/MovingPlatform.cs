using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Rigidbody rb;
    private bool movingToB = true;

    private Vector3 previousPosition;
    public Vector3 DeltaPosition { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

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

    private void OnDrawGizmosSelected()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
