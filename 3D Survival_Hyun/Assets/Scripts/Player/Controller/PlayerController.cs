using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 입력 처리, 이동/회전 제어, 낙하 데미지 등을 통합 관리하는 컨트롤러 클래스
/// 주요 기능은 각 전용 컴포넌트(PlayerLook, PlayerMovement 등)에 위임함
/// </summary>
public class PlayerController : MonoBehaviour
{
    private PlayerLook look;
    private PlayerMovement movement;
    private PlayerFallDamage fallDamage;
    private PlayerInputHandler input;

    /// <summary>
    /// 관련 컴포넌트 참조 초기화 
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
    /// 물리 연산 처리: 낙하 데미지 체크
    /// </summary>
    private void FixedUpdate()
    {
        fallDamage.CheckFallDamage();
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

    /// <summary>
    /// 이동 입력 처리
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        input.OnMoveInput(context);
    }

    /// <summary>
    /// 점프 입력 처리
    /// </summary>
    /// <param name="context"></param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        input.OnJumpInput(context);
    }

    /// <summary>
    /// 마우스 커서 잠금 상태를 토글하고, 시점 제어도 동기화
    /// </summary>
    /// <param name="toggle">true면 커서 해제, false면 잠금</param>
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        look.ToggleLook(!toggle);
    }
}