using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DeliveryRecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> LsIngredient;
    public string Name;
}
