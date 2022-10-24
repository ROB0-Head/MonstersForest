using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    
    public float NormInputX { get; private set; }
    public float NormInputY { get; private set; }
    public bool JumpInput { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        RawMovementInput = ctx.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }
    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        JumpInput = false;
        if (ctx.performed)
        {
            JumpInput = true;
        }
    }
}
