using System;
using System.Collections.Generic;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedArgs> OnIngredientAdded;

    public class OnIngredientAddedArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

    [UnityEngine.SerializeField] private List<KitchenObjectSO> _validKitchenObjects = new();

    private readonly List<KitchenObjectSO> _lsKitchenObject = new();

    public List<KitchenObjectSO> LsKitchenObject => _lsKitchenObject;

    public bool TryAddIngredient(KitchenObjectSO obj)
    {
        if (!_validKitchenObjects.Contains(obj))
        {
            //Not valid ingredient
            return false;
        }

        if (_lsKitchenObject.Contains(obj))
        {
            //Already has this type of kitchen obj
            return false;
        }
        else
        {
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedArgs
            {
                KitchenObjectSO = obj
            });

            _lsKitchenObject.Add(obj);
            return true;
        }
    }
}
