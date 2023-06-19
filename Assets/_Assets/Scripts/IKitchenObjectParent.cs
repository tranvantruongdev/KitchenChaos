public interface IKitchenObjectParent {
    public void ClearKitchenObject();

    public bool HasKitchenObject();
    public KitchenObject GetKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public UnityEngine.Transform GetSpamPoint();
}
