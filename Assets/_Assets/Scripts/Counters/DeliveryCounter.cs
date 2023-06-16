using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player == null) return;

        if (player.GetKitchenObject().TryGetKitchenObjOfType(out PlateKitchenObject plate))
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
