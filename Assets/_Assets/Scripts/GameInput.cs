using UnityEngine;
using System;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteractAction;

    private Vector2 moveDir;
    private PlayerInputAction inputActions;

    private void Start() {
        inputActions = new PlayerInputAction();
        inputActions.Player.Move.Enable();

        inputActions.Player.Interact.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNomalizedDirVector() {
        this.moveDir = inputActions.Player.Move.ReadValue<Vector2>();

        return moveDir;
    }
}
