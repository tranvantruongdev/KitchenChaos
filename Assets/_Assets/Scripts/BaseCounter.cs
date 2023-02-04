using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjcectParent {
    [SerializeField] private Transform objSpamPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {

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
