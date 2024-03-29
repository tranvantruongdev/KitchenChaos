using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager S_Instance;

    public event System.EventHandler OnOderAdded;
    public event System.EventHandler OnOderDeliver;

    public class DeliveryStatusArgs : System.EventArgs
    {
        public bool IsSuccess;
    }

    public List<DeliveryRecipeSO> LsDeliveryRecipeSO { get => _lsDeliveryRecipeSO; }

    [SerializeField] private DeliveryRecipeListSO _deliveryRecipeListSO;
    [SerializeField] private int _maxRequiredRecipe = 4;
    [SerializeField] private float _interval = 4;

    private List<DeliveryRecipeSO> _lsDeliveryRecipeSO;
    private float _timer;

    private void Awake()
    {
        S_Instance = this;
        _lsDeliveryRecipeSO = new List<DeliveryRecipeSO>();
        _timer = _interval;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _interval && _lsDeliveryRecipeSO.Count < _maxRequiredRecipe)
        {
            _timer = 0;
            List<DeliveryRecipeSO> lsDeliveryRecipeSO = _deliveryRecipeListSO.LsDeliveryRecipeSO;
            DeliveryRecipeSO randomRecipeSO = lsDeliveryRecipeSO[Random.Range(0, lsDeliveryRecipeSO.Count)];
            _lsDeliveryRecipeSO.Add(randomRecipeSO);
            Debug.Log(randomRecipeSO.name);
            OnOderAdded?.Invoke(this, System.EventArgs.Empty);
        }
    }

    public void DeliverPlate(PlateKitchenObject plateKitchenObject)
    {
        var deliveryStatusArgs = new DeliveryStatusArgs();

        if (HasRequiredRecipe(plateKitchenObject))
        {
            deliveryStatusArgs.IsSuccess = true;
        }
        else
        {
            deliveryStatusArgs.IsSuccess = false;
        }

        OnOderDeliver?.Invoke(this, deliveryStatusArgs);
    }

    private bool HasRequiredRecipe(PlateKitchenObject plateKitchenObject)
    {
        bool isMatchRequiredRecipe = false;
        // Loop list of required recipe
        foreach (var deliveryRecipeSO in _lsDeliveryRecipeSO)
        {
            if (deliveryRecipeSO.LsIngredient.Count != plateKitchenObject.LsKitchenObject.Count)
            {
                // Bypass unmatched kitchen object number in ingredient list
                continue;
            }
            // Loop ingredient list of each recipe
            foreach (var ingredient in deliveryRecipeSO.LsIngredient)
            {
                bool matchedFound = false;
                // Loop all ingredient in plate
                foreach (var ingredientInPlate in plateKitchenObject.LsKitchenObject)
                {
                    if (ingredient.Equals(ingredientInPlate))
                    {
                        // Break if any match found
                        matchedFound = true;
                        break;
                    }
                }
                // If ingredient in plate does not match with any ingredient in recipe then break from checking this recipe
                if (!matchedFound)
                {
                    break;
                }
                // Finally if no unmatched found, return true
                _ = _lsDeliveryRecipeSO.Remove(deliveryRecipeSO);
                return true;
            }
        }

        return isMatchRequiredRecipe;
    }
}
