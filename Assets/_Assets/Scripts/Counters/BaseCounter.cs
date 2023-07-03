using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private Transform objSpamPoint;

    public static event EventHandler OnPickupObj;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {

    }
    public virtual void InteractAlt() {

    }

    public void ClearKitchenObject() {
        kitchenObject = null;
        OnPickupObj?.Invoke(this, EventArgs.Empty);
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
