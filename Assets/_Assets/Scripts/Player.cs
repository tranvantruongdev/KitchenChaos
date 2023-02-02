using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float speed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;

    private Vector2 moveDir;
    private bool isWalking;
    private bool isCanMove;
    private float playerHeight = 2f;
    private float playerRadius = 0.7f;
    private float moveDistance;
    private float interactDistance = 2f;
    private Vector3 previousDir;

    public bool IsWalking { get => isWalking;}

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        moveDir = gameInput.GetNomalizedDirVector();

        if (moveDir != Vector2.zero) {
            previousDir = new(moveDir.x, 0, moveDir.y);
        }

        if (Physics.Raycast(transform.position, previousDir, out RaycastHit raycastHit, interactDistance, layerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                Debug.Log(clearCounter.transform);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        HandleMovement();
        //HandleInteraction();
    }

    private void HandleInteraction() {
        if (moveDir != Vector2.zero) {
            previousDir = new(moveDir.x, 0, moveDir.y);
        }

        if (Physics.Raycast(transform.position, previousDir, out RaycastHit raycastHit, interactDistance, layerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                Debug.Log(clearCounter.transform);
            }
        }
    }

    private void HandleMovement() {
        this.moveDir = gameInput.GetNomalizedDirVector();
        moveDistance = Time.deltaTime * speed;
        Vector3 moveDir = new(this.moveDir.x, 0, this.moveDir.y);
        isCanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!isCanMove) {
            Vector3 moveDirX = new(moveDir.x, 0, 0);
            isCanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (isCanMove) {
                moveDir = moveDirX.normalized;
            } else {
                Vector3 moveDirZ = new(0, 0, moveDir.z);
                isCanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (isCanMove) {
                    moveDir = moveDirZ.normalized;
                }
            }
        }

        if (isCanMove) {
            transform.position += speed * Time.deltaTime * moveDir;
        }

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);

        isWalking = moveDir != Vector3.zero;
    }
}
