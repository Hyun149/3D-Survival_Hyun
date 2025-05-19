using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 대쉬 기능을 제어하는 클래스
/// </summary>
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashStaminaCost = 30f;
    [SerializeField] private StaminaSystem staminaSystem;

    private Rigidbody rb;
    private bool canDash = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        staminaSystem = GetComponent<StaminaSystem>();
    }

    public void TryDash(Vector3 direction)
    {
        if (!canDash)
        {
            return;
        }
        if (!staminaSystem.Consume(dashStaminaCost))
        {
            return;
        }

        rb.velocity = direction.normalized * dashForce;
        GetComponent<PlayerMovement>()?.NotifyDshStart();
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
