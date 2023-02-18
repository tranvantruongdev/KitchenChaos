using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject {
    public KitchenObjectSO from;
    public KitchenObjectSO to;
    public float maximumProgress;
}
