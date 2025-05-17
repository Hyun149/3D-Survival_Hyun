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
    public Vector2 MovementInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public Vector2 LookInput { get; private set; }

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
    }

    /// <summary>
    /// ���콺 �Ǵ� ��ƽ ���� �Է� ó��
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
}
