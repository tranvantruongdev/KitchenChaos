using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjcectParent kitchenObjectParent;

    public KitchenObjectSO KitchenObjectSO { get => kitchenObjectSO; }

    public void SetKitchenObjcectParent(IKitchenObjcectParent kitchenObjectParent)
    {
        this.kitchenObjectParent?.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("The counter already has kitchen object");
        }

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetSpamPoint();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject CreateKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjcectParent kitchenObjcectParent)
    {
        KitchenObject kitchenObject = Instantiate(kitchenObjectSO.prefab).GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjcectParent(kitchenObjcectParent);

        return kitchenObject;
    }

    public bool TryGetKitchenObjOfType<T>(out T obj) where T : KitchenObject
    {
        if (this is T)
        {
            obj = this as T;
            return true;
        }
        else
        {
            obj = null;
            return false;
        }
    }
}
