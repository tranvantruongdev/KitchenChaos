using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArr;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //there is no kitchen object on this counter
            if (player.HasKitchenObject()) {
                //player is carrying invalid kitchen object
                var recipe = GetOutputFromInput(player.GetKitchenObject().KitchenObjectSO);
                if (recipe == null) {
                    return;
                }

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
            var recipe = GetOutputFromInput(GetKitchenObject().KitchenObjectSO);
            if (recipe == null) {
                return;
            }

            GetKitchenObject().DestroySelf();
            KitchenObject.CreateKitchenObject(recipe, this);
        }
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectSO) {
        foreach (var recipe in cuttingRecipeSOArr) {
            if (recipe.from == kitchenObjectSO) {
                return recipe.to;
            }
        }

        return null;
    }
}
