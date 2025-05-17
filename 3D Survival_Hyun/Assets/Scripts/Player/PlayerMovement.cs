using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;


    private Rigidbody rb;
    private PlayerInputHandler input;
    private GroundChecker groundChecker;
    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInputHandler>();
        groundChecker = GetComponent<GroundChecker>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void FixedUpdate()
    {
        Move();

        if (input.JumpPressed && groundChecker.IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            playerAnimator?.PlayJump();
        }

        input.ClearInput();
    }

    /// <summary>
    /// 입력된 방향에 따라 Rigidbody
    /// </summary>
    private void Move()
    {
        Vector3 direction = transform.forward * input.MovementInput.y + transform.right * input.MovementInput.x;
        direction *= moveSpeed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        if (playerAnimator != null)
        {
            bool isRunning = input.MovementInput.magnitude > 0.1f;
            playerAnimator.SetRunning(isRunning);
        }
    }

    public void ApplyJumpBoost(float amount, float duration)
    {
        StartCoroutine(JumpBoostRoutine(amount, duration));
    }

    private IEnumerator JumpBoostRoutine(float amount, float duration)
    {
        jumpPower += amount;
        yield return new WaitForSeconds(duration);
        jumpPower -= amount;
    }
}
