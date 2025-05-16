using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 이동, 점프, 마우스 회전 제어를 담당하는 컨트롤러 클래스
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;
    private float fallDamageThreshold = -15f;
    private float previousYVelocity = 0f;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rb;

    /// <summary>
    /// 컴포넌트 초기화 (Rigidbody 할당)
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 게임 시작 시 마우스 커서를 잠금 상태로 설정
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// FixedUpdated에서 물리 기반 이동 처리
    /// </summary>
    private void FixedUpdate()
    {
        CheckFallDamage();
        Move();
    }

    /// <summary>
    /// 카메라 회전은 LateUpdate에서 처리 (시점 흔들림 방지용)
    /// </summary>
    private void LateUpdate()
    {
        if (canLook)
        {
            Debug.Log($"[Look] delta: {mouseDelta}");
            CameraLook();
        }
    }

    void CheckFallDamage()
    {
        if (IsGrounded())
        {
            if (previousYVelocity < fallDamageThreshold)
            {
                float damage = Mathf.Abs(previousYVelocity) * 2f;
                GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }

            previousYVelocity = 0f;
        }
        else
        {
            previousYVelocity = rb.velocity.y;
        }
    }

    /// <summary>
    /// 마우스 입력을 처리하는 메서드    
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 이동 입력을 처리하는 메서드
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        curMovementInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 점프 입력을 처리하는 메서드
    /// </summary>
    /// <param name="context"></param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 이동 처리 메서드
    /// </summary>
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    /// <summary>
    /// 카메라 회전 처리 메서드
    /// </summary>
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// 바닥에 닿아 있는지 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 마우스 커서 잠금 상태를 토글하는 메서드
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}