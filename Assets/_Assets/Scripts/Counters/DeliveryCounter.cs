public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player == null) return;

        if (!player.HasKitchenObject())
        {
            return;
        }

        if (player.GetKitchenObject().TryGetKitchenObjOfType(out PlateKitchenObject _))
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
