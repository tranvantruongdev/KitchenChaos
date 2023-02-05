public class ClearCounter : BaseCounter {
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
}
