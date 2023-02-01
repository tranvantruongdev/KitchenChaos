using UnityEngine;

public class GameInput : MonoBehaviour {
    private Vector2 moveDir;
    private PlayerInputAction inputActions;

    private void Awake() {
        inputActions = new PlayerInputAction();
        inputActions.Player.Move.Enable();
    }

    public Vector2 GetNomalizedDirVector() {
        this.moveDir = inputActions.Player.Move.ReadValue<Vector2>();

        return moveDir;
    }
}
