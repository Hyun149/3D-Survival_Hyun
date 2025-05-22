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
    [SerializeField] private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction dashAction;

    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool DashPressed { get; private set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var actionMap = playerInput.actions.FindActionMap("Player", true);

        moveAction = actionMap.FindAction("Move", true);
        lookAction = actionMap.FindAction("Look", true);
        jumpAction = actionMap.FindAction("Jump", true);
        dashAction = actionMap.FindAction("Dash", true);
    }

    

    private void OnEnable()
    {
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;

        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;

        jumpAction.started += OnJumpStarted;
        dashAction.started += OnDashStarted;

        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        dashAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;

        lookAction.performed -= OnLookPerformed;
        lookAction.canceled -= OnLookCanceled;

        jumpAction.started -= OnJumpStarted;
        dashAction.started -= OnDashStarted;

        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        dashAction.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx) => MovementInput = ctx.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext ctx) => MovementInput = Vector2.zero;

    private void OnLookPerformed(InputAction.CallbackContext ctx) => LookInput = ctx.ReadValue<Vector2>();
    private void OnLookCanceled(InputAction.CallbackContext ctx) => LookInput = Vector2.zero;

    private void OnJumpStarted(InputAction.CallbackContext ctx) => JumpPressed = true;
    private void OnDashStarted(InputAction.CallbackContext ctx) => DashPressed = true;

    /// <summary>
    /// 외부에서 매 프레임 입력 상태를 초기화할 때 사용
    /// (Jump/Dash 입력은 "한 프레임만 유지"되는 구조)
    /// </summary>
    public void ClearInput()
    {
        JumpPressed = false;
        DashPressed = false;
    }
}
