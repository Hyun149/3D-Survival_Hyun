using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 입력(이동, 점프, 대시, 시점 회전)을 처리하고
/// 해당 상태를 저장하여 외부에서 접근 가능하도록 제공하는 클래스
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
    /// 이동 입력 처리: WASD, 스틱 입력 등
    /// </summary>
    /// <param name="context">입력 컨텍스트</param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 점프 키 입력 처리: 눌린 순간에 JumpPressed를 true로 설정
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
    /// 프레임마다 점프 입력 상태를 초기화하여 중복 처리 방지
    /// (예: 점프, 대시는 누른 순간만 감지)
    /// </summary>
    public void ClearInput()
    {
        JumpPressed = false;
        DashPressed = false;
    }

    /// <summary>
    /// 시점 회전 입력 처림: 마우스 또는 조이스틱의 Look 입력
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 대시 키 입력 처리: 눌린 순간에 DashPressed를 true로 설정
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
