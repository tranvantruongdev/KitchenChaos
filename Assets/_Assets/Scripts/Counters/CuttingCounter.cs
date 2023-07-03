using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArr;

    public event EventHandler<IHasProgress.ProgressArgs> OnProgressChanged;
    public static event EventHandler OnCutting;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object on this counter
            if (player.HasKitchenObject())
            {
                //player is carrying invalid kitchen object
                var recipe = GetOutputFromInput(player.GetKitchenObject().KitchenObjectSO);
                if (recipe == null)
                {
                    return;
                }

                //place kitchen object on this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }
            else
            {
                //only give kitchen object to player when he doesnt hold anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlt()
    {
        if (HasKitchenObject())
        {
            //there is kitchen object on this counter
            KitchenObjectSO kitchenObjectSO = GetKitchenObject().KitchenObjectSO;
            var recipe = GetOutputFromInput(kitchenObjectSO);
            if (recipe == null)
            {
                return;
            }

            cuttingProgress++;
            int maximumProgress = GetOutputSOFromInput(kitchenObjectSO).maximumProgress;
            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressArgs { progress = (float)cuttingProgress / maximumProgress });
            OnCutting?.Invoke(this, EventArgs.Empty);
            //havent done cutting yet
            if (cuttingProgress < maximumProgress)
            {
                return;
            }

            GetKitchenObject().DestroySelf();
            KitchenObject.CreateKitchenObject(recipe, this);
            //reset progress
            cuttingProgress = 0;
            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressArgs { progress = 0 });
        }
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var recipe in cuttingRecipeSOArr)
        {
            if (recipe.from == kitchenObjectSO)
            {
                return recipe.to;
            }
        }

        return null;
    }

    private CuttingRecipeSO GetOutputSOFromInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var recipe in cuttingRecipeSOArr)
        {
            if (recipe.from == kitchenObjectSO)
            {
                return recipe;
            }
        }

        return null;
    }
}
