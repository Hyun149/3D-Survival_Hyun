using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾��� �Է�(�̵�, ����, ���, ���� ȸ��)�� ó���ϰ�
/// �ش� ���¸� �����Ͽ� �ܺο��� ���� �����ϵ��� �����ϴ� Ŭ����
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
    /// �̵� �Է� ó��: WASD, ��ƽ �Է� ��
    /// </summary>
    /// <param name="context">�Է� ���ؽ�Ʈ</param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ���� Ű �Է� ó��: ���� ������ JumpPressed�� true�� ����
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
    /// �����Ӹ��� ���� �Է� ���¸� �ʱ�ȭ�Ͽ� �ߺ� ó�� ����
    /// (��: ����, ��ô� ���� ������ ����)
    /// </summary>
    public void ClearInput()
    {
        JumpPressed = false;
        DashPressed = false;
    }

    /// <summary>
    /// ���� ȸ�� �Է� ó��: ���콺 �Ǵ� ���̽�ƽ�� Look �Է�
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ��� Ű �Է� ó��: ���� ������ DashPressed�� true�� ����
    /// </summary>
    /// <param name="context"></param>
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashPressed = true;
        }
    }
}
