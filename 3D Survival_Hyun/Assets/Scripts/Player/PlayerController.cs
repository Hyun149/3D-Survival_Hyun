using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾� �̵�, ����, ���콺 ȸ�� ��� ����ϴ� ��Ʈ�ѷ� Ŭ����
/// </summary>
public class PlayerController : MonoBehaviour
{
    private PlayerLook look;
    private PlayerMovement movement;
    private PlayerFallDamage fallDamage;
    private PlayerInputHandler input;

    /// <summary>
    /// ������Ʈ �ʱ�ȭ (Rigidbody �Ҵ�)
    /// </summary>
    private void Awake()
    {
        look = GetComponent<PlayerLook>();
        movement = GetComponent<PlayerMovement>();
        fallDamage = GetComponent<PlayerFallDamage>();
        input = GetComponent<PlayerInputHandler>();
    }

    /// <summary>
    /// ���� ���� �� ���콺 Ŀ���� ��� ���·� ����
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    /// <summary>
    /// ī�޶� ȸ���� LateUpdate���� ó�� (���� ��鸲 ������)
    /// </summary>
    private void LateUpdate()
    {
        look.HandleLook();
    }


    /// <summary>
    /// ���콺 �Է��� ó���ϴ� �޼���    
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        input.OnLookInput(context);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        input.OnMoveInput(context);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        input.OnJumpInput(context);
    }

    /// <summary>
    /// ���콺 Ŀ�� ��� ���¸� ����ϴ� �޼���
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        look.ToggleLook(!toggle);
    }
}