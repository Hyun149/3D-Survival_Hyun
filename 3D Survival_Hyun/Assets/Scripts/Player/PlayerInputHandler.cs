using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 이동, 점프, 시점 입력을 처리하고 해당 상태를 저장하는 클래스
/// PlayerController 등 외부에서 접근하여 입력 상태를 활용함
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
    /// 이동 입력을 처리하여 MovementInput에 저장
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 점프 키가 눌렸을 때 JumpPressed를 True로 설정
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
    /// 프레임마다 점프 입력 상태를 초기화
    /// </summary>
    public void ClearInput()
    {
        JumpPressed = false;
        DashPressed = false;
    }

    /// <summary>
    /// 마우스 또는 스틱 시점 입력 처리
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
