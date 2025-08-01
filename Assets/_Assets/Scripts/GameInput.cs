using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputSystem_Actions playerInputActions;

    public event EventHandler OnJumpInput;

    private void Awake() {
        Instance = this;

        playerInputActions = new InputSystem_Actions();
        playerInputActions.Enable();

        playerInputActions.Player2D.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext context) {
        OnJumpInput?.Invoke(this, EventArgs.Empty);
    }

    public float GetMovementInput() {
        float input = playerInputActions.Player2D.Move.ReadValue<float>();

        return input;
    }
}
