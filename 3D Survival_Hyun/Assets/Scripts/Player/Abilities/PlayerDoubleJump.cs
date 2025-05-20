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

    private Rigidbody rb;
    private StaminaSystem staminaSystem;
    public bool HasDoubleJumped { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        staminaSystem = GetComponent<StaminaSystem>();
    }

    public bool TryDoubleJump()
    {
        if (HasDoubleJumped) return false;
        if (!staminaSystem.Consume(StaminaCost)) return false;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
        HasDoubleJumped = true;
        return true;
    }

    public void ResetDoubleJump()
    {
        HasDoubleJumped = false;
    }
}
