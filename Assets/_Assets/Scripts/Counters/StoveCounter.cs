using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<FryStateChangedArgs> OnFryStateChanged;
    public event EventHandler<IHasProgress.ProgressArgs> OnProgressChanged;

    public class FryStateChangedArgs : EventArgs
    {
        public State state;
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArr;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArr;

    public enum State
    {
        Idle,
        Frying,
        Cooked,
        Burned,
    }

    private float timer;
    private FryingRecipeSO fryingRecipeSO;
    private State cookingState;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        cookingState = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (cookingState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    timer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressArgs
                    {
                        progress = timer / fryingRecipeSO.maximumProgress,
                    });

                    if (timer > fryingRecipeSO.maximumProgress)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject cooked = KitchenObject.CreateKitchenObject(fryingRecipeSO.to, this);
                        burningRecipeSO = GetBurningSOFromInput(cooked.KitchenObjectSO);
                        cookingState = State.Cooked;
                        timer = 0;
                        OnFryStateChanged?.Invoke(this, new FryStateChangedArgs
                        {
                            state = cookingState
                        });
                    }
                    break;
                case State.Cooked:
                    timer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressArgs
                    {
                        progress = timer / burningRecipeSO.maximumProgress,
                    });

                    if (timer > burningRecipeSO.maximumProgress)
                    {
                        GetKitchenObject().DestroySelf();

                        _ = KitchenObject.CreateKitchenObject(burningRecipeSO.to, this);
                        cookingState = State.Burned;
                        timer = 0;
                        OnFryStateChanged?.Invoke(this, new FryStateChangedArgs
                        {
                            state = cookingState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.ProgressArgs
                        {
                            progress = 0f,
                        });
                    }
                    break;
                case State.Burned:
                    break;
                default:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object on this counter
            if (player.HasKitchenObject())
            {
                //player is carrying invalid kitchen object
                KitchenObjectSO kitchenObjectSO = player.GetKitchenObject().KitchenObjectSO;
                KitchenObjectSO recipe = GetOutputFromInput(kitchenObjectSO);
                if (recipe == null)
                {
                    return;
                }

                //place kitchen object on this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipeSO = GetOutputSOFromInput(kitchenObjectSO);
                cookingState = State.Frying;
                OnFryStateChanged?.Invoke(this, new FryStateChangedArgs
                {
                    state = cookingState
                });
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

                        cookingState = State.Idle;

                        OnFryStateChanged?.Invoke(this, new FryStateChangedArgs
                        {
                            state = cookingState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.ProgressArgs
                        {
                            progress = 0f,
                        });
                    }
                }
            }
            else
            {
                //only give kitchen object to player when he doesnt hold anything
                GetKitchenObject().SetKitchenObjectParent(player);
                cookingState = State.Idle;
                OnFryStateChanged?.Invoke(this, new FryStateChangedArgs
                {
                    state = cookingState
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.ProgressArgs
                {
                    progress = 0f,
                });
            }

            timer = 0f;
        }
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO recipe in fryingRecipeSOArr)
        {
            if (recipe.from == kitchenObjectSO)
            {
                return recipe.to;
            }
        }

        return null;
    }

    private FryingRecipeSO GetOutputSOFromInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO recipe in fryingRecipeSOArr)
        {
            if (recipe.from == kitchenObjectSO)
            {
                return recipe;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningSOFromInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurningRecipeSO recipe in burningRecipeSOArr)
        {
            if (recipe.from == kitchenObjectSO)
            {
                return recipe;
            }
        }

        return null;
    }
}
