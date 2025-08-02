using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputSystem_Actions playerInputActions;

    public event EventHandler OnJumpInput;
    public event EventHandler OnAttackInput;
    public event EventHandler OnItemInput;

    private void Awake() {
        Instance = this;

        playerInputActions = new InputSystem_Actions();
        playerInputActions.Enable();

    }

    private void Start() {
        playerInputActions.Player2D.Jump.performed += Jump_performed;
        playerInputActions.Player2D.Attack.performed += Attack_performed;
        playerInputActions.Player2D.Item1.performed += Item_performed;

    }

    private void Item_performed(InputAction.CallbackContext context) {
        OnItemInput?.Invoke(this, EventArgs.Empty); 
    }

    private void Attack_performed(InputAction.CallbackContext context) {
        OnAttackInput?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(InputAction.CallbackContext context) {
        OnJumpInput?.Invoke(this, EventArgs.Empty);
    }

    public float GetMovementInput() {
        float input = playerInputActions.Player2D.Move.ReadValue<float>();

        return input;
    }
}
