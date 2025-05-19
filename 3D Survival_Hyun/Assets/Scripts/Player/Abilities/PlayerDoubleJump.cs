using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 더블 점프를 제어하는 클래스
/// </summary>
public class PlayerDoubleJump : MonoBehaviour
{
    [SerializeField] private float doubleJumpForce = 7f;
    [SerializeField] private float StaminaCost = 20f;
    [SerializeField] private GroundChecker groundChecker;

    private Rigidbody rb;
    private StaminaSystem staminaSystem;
    private bool hasDoubleJumped = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        staminaSystem = GetComponent<StaminaSystem>();
        groundChecker = GetComponent<GroundChecker>();
    }

    public void TryJump()
    {
        if (hasDoubleJumped)
        {
            return;
        }
        if (!staminaSystem.Consume(StaminaCost))
        {
            return;
        }

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
        hasDoubleJumped = true;
    }

    private void FixedUpdate()
    {
        if (groundChecker.IsGrounded())
        {
            hasDoubleJumped = false;
        }
    }

    private bool isGround => Physics.Raycast(transform.position, Vector3.down, 1.1f);
}
