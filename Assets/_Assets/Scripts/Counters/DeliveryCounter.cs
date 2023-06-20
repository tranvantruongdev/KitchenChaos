public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player == null) return;

        if (!player.HasKitchenObject())
        {
            return;
        }

        if (!player.GetKitchenObject().TryGetKitchenObjOfType(out PlateKitchenObject plate))
        {
            return;
        }

        DeliveryManager.instance.DeliverPlate(plate);
        player.GetKitchenObject().DestroySelf();
    }
}
