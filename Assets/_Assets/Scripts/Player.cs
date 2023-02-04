using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjcectParent {
    public static Player Instance { get; private set; }

    [SerializeField] private float speed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform spamPoint;

    public event EventHandler<OnSelectCounterChangedArgs> OnSelectCounterChanged;
    public class OnSelectCounterChangedArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    private Vector2 moveDir;
    private bool isWalking;
    private bool isCanMove;
    private readonly float playerHeight = 2f;
    private readonly float playerRadius = 0.7f;
    private float moveDistance;
    private readonly float interactDistance = 2f;
    private Vector3 previousDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    public bool IsWalking { get => isWalking; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is something wrong");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        } else {
            Debug.Log("Null counter");
        }
    }

    // Update is called once per frame
    private void Update() {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction() {
        if (moveDir != Vector2.zero) {
            previousDir = new(moveDir.x, 0, moveDir.y);
        }

        if (Physics.Raycast(transform.position, previousDir, out RaycastHit raycastHit, interactDistance, layerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                SetSelectedCounter(baseCounter);
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter baseCounter) {
        selectedCounter = baseCounter;
        OnSelectCounterChanged?.Invoke(this, new OnSelectCounterChangedArgs { selectedCounter = selectedCounter });
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

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public Transform GetSpamPoint() {
        return spamPoint;
    }
}
