using System;
using System.Collections.Generic;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngridentAddedArgs> OnIngridentAdded;

    public class OnIngridentAddedArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

    [UnityEngine.SerializeField] private List<KitchenObjectSO> _validKitchenObjects = new();

    private readonly List<KitchenObjectSO> _kitchenObjects = new();

    public bool TryAddIngridient(KitchenObjectSO obj)
    {
        if (!_validKitchenObjects.Contains(obj))
        {
            //Not valid ingrident
            return false;
        }

        if (_kitchenObjects.Contains(obj))
        {
            //Already has this type of kitchen obj
            return false;
        }
        else
        {
            OnIngridentAdded?.Invoke(this, new OnIngridentAddedArgs
            {
                KitchenObjectSO = obj
            });

            _kitchenObjects.Add(obj);
            return true;
        }
    }
}
