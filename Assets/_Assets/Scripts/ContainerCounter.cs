using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    public event EventHandler OnPlayerGrabbedKitchenObject;

    [SerializeField] private KitchenObjectSO kitchenObj;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //player dont hold anything
            var obj = Instantiate(kitchenObj.prefab);
            obj.GetComponent<KitchenObject>().SetKitchenObjcectParent(player);
            OnPlayerGrabbedKitchenObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
