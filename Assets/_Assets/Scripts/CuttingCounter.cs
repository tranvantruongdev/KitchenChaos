using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjSO;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //there is no kitchen object on this counter
            if (player.HasKitchenObject()) {
                //place kitchen object on this counter
                player.GetKitchenObject().SetKitchenObjcectParent(this);
            } else {
                //player has nothing to give
            }
        } else {
            if (player.HasKitchenObject()) {
                //player is carring kitchen object
            } else {
                //only give kitchen object to player when he doesnt hold anything
                GetKitchenObject().SetKitchenObjcectParent(player);
            }
        }
    }

    public override void InteractAlt() {
        if (HasKitchenObject()) {
            //there is kitchen object on this counter
            GetKitchenObject().DestroySelf();
            KitchenObject.CreateKitchenObject(kitchenObjSO, this);
        }
    }
}
