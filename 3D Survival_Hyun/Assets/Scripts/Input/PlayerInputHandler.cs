using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾��� �̵�, ����, ���� �Է��� ó���ϰ� �ش� ���¸� �����ϴ� Ŭ����
/// PlayerController �� �ܺο��� �����Ͽ� �Է� ���¸� Ȱ����
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerDoubleJump playerDoubleJump;

    public Vector2 MovementInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public Vector2 LookInput { get; private set; }

    public bool DashPressed { get; private set; }

    /// <summary>
    /// �̵� �Է��� ó���Ͽ� MovementInput�� ����
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ���� Ű�� ������ �� JumpPressed�� True�� ����
    /// </summary>
    /// <param name="context"></param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpPressed = true;
        }
    }

    /// <summary>
    /// �����Ӹ��� ���� �Է� ���¸� �ʱ�ȭ
    /// </summary>
    public void ClearInput()
    {
        JumpPressed = false;
        DashPressed = false;
    }

    /// <summary>
    /// ���콺 �Ǵ� ��ƽ ���� �Է� ó��
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashPressed = true;
        }
    }
}
