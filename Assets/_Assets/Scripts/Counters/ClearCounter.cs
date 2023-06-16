public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object on this counter
            if (player.HasKitchenObject())
            {
                //place kitchen object on this counter
                player.GetKitchenObject().SetKitchenObjcectParent(this);
            }
            else
            {
                //player has nothing to give
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //player is carring kitchen object
                if (player.GetKitchenObject().TryGetKitchenObjOfType(out PlateKitchenObject plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().KitchenObjectSO))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else if (GetKitchenObject().TryGetKitchenObjOfType(out plate))
                {
                    //There is a plate on this counter
                    if (plate.TryAddIngredient(player.GetKitchenObject().KitchenObjectSO))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                //only give kitchen object to player when he doesnt hold anything
                GetKitchenObject().SetKitchenObjcectParent(player);
            }
        }
    }
}
