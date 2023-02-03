using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjcectParent {
    [SerializeField] private GameObject counterVisual;
    [SerializeField] private KitchenObjectSO kitchenObj;
    [SerializeField] private Transform objSpamPoint;

    private KitchenObject kitchenObject;

    public Transform ObjSpamPoint { get => objSpamPoint; }

    private void Start() {
        Player.Instance.OnSelectCounterChanged += Player_OnSelectCounterChanged;
    }

    private void Player_OnSelectCounterChanged(object sender, Player.OnSelectCounterChangedArgs e) {
        if (e.selectedCounter == this) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        counterVisual.SetActive(true);
    }

    private void Hide() {
        counterVisual.SetActive(false);
    }

    public void Interact(Player player) {
        if (kitchenObject == null) {
            var obj = Instantiate(kitchenObj.prefab, objSpamPoint);
            obj.GetComponent<KitchenObject>().SetKitchenObjcectParent(this);
        } else {
            kitchenObject.SetKitchenObjcectParent(player);
        }
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
        return objSpamPoint;
    }
}
