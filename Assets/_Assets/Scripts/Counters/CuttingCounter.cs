using System;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    public event EventHandler<CuttingProgressArgs> OnCuttingProgressChanged;
    public class CuttingProgressArgs : EventArgs {
        public float progress;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArr;

    private int cuttingProgress;

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
            KitchenObjectSO kitchenObjectSO = GetKitchenObject().KitchenObjectSO;
            var recipe = GetOutputFromInput(kitchenObjectSO);
            if (recipe == null) {
                return;
            }

            cuttingProgress++;
            int maximumProgress = GetOutputSOFromInput(kitchenObjectSO).maximumProgress;
            OnCuttingProgressChanged?.Invoke(this, new CuttingProgressArgs { progress = (float)cuttingProgress / maximumProgress });
            //havent done cutting yet
            if (cuttingProgress < maximumProgress) {
                return;
            }

            GetKitchenObject().DestroySelf();
            KitchenObject.CreateKitchenObject(recipe, this);
            //reset progress
            cuttingProgress = 0;
            OnCuttingProgressChanged?.Invoke(this, new CuttingProgressArgs { progress = 0 });
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

    private CuttingRecipeSO GetOutputSOFromInput(KitchenObjectSO kitchenObjectSO) {
        foreach (var recipe in cuttingRecipeSOArr) {
            if (recipe.from == kitchenObjectSO) {
                return recipe;
            }
        }

        return null;
    }
}
