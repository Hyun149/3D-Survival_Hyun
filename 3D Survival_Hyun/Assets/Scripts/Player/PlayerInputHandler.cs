using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public Vector2 LookInput { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpPressed = true;
        }
    }

    public void ClearInput()
    {
        JumpPressed = false;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
}
