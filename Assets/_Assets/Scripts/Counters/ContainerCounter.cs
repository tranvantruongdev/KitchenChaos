using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    public event EventHandler OnPlayerGrabbedKitchenObject;

    [SerializeField] private KitchenObjectSO kitchenObj;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //player dont hold anything
            KitchenObject.CreateKitchenObject(kitchenObj, player);
            OnPlayerGrabbedKitchenObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
