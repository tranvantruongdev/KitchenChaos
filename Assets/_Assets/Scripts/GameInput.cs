using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAltAction;

    private Vector2 moveDir;
    private PlayerInputAction inputActions;

    private void Start() {
        inputActions = new PlayerInputAction();
        inputActions.Player.Move.Enable();

        inputActions.Player.Interact.Enable();
        inputActions.Player.Interact.performed += Interact_performed;

        inputActions.Player.InteractAlt.Enable();
        inputActions.Player.InteractAlt.performed += InteractAlt_performed; ;
    }

    private void InteractAlt_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAltAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNomalizedDirVector() {
        this.moveDir = inputActions.Player.Move.ReadValue<Vector2>();

        return moveDir;
    }
}
