using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾� �̵�, ����, ���콺 ȸ�� ��� ����ϴ� ��Ʈ�ѷ� Ŭ����
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
    /// ������Ʈ �ʱ�ȭ (Rigidbody �Ҵ�)
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ���� ���� �� ���콺 Ŀ���� ��� ���·� ����
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// FixedUpdated���� ���� ��� �̵� ó��
    /// </summary>
    private void FixedUpdate()
    {
        CheckFallDamage();
        Move();
    }

    /// <summary>
    /// ī�޶� ȸ���� LateUpdate���� ó�� (���� ��鸲 ������)
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
    /// ���콺 �Է��� ó���ϴ� �޼���    
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// �̵� �Է��� ó���ϴ� �޼���
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        curMovementInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ���� �Է��� ó���ϴ� �޼���
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
    /// �̵� ó�� �޼���
    /// </summary>
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    /// <summary>
    /// ī�޶� ȸ�� ó�� �޼���
    /// </summary>
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// �ٴڿ� ��� �ִ��� Ȯ���ϴ� �޼���
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
    /// ���콺 Ŀ�� ��� ���¸� ����ϴ� �޼���
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}