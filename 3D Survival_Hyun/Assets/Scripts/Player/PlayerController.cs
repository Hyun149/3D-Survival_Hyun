using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 이동, 점프, 마우스 회전 제어를 담당하는 컨트롤러 클래스
/// </summary>
public class PlayerController : MonoBehaviour
{
    private PlayerLook look;
    private PlayerMovement movement;
    private PlayerFallDamage fallDamage;
    private PlayerInputHandler input;

    /// <summary>
    /// 컴포넌트 초기화 (Rigidbody 할당)
    /// </summary>
    private void Awake()
    {
        look = GetComponent<PlayerLook>();
        movement = GetComponent<PlayerMovement>();
        fallDamage = GetComponent<PlayerFallDamage>();
        input = GetComponent<PlayerInputHandler>();
    }

    /// <summary>
    /// 게임 시작 시 마우스 커서를 잠금 상태로 설정
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    /// <summary>
    /// 카메라 회전은 LateUpdate에서 처리 (시점 흔들림 방지용)
    /// </summary>
    private void LateUpdate()
    {
        look.HandleLook();
    }


    /// <summary>
    /// 마우스 입력을 처리하는 메서드    
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
    /// 마우스 커서 잠금 상태를 토글하는 메서드
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        look.ToggleLook(!toggle);
    }
}