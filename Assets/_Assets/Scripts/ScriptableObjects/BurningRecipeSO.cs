using UnityEngine;

[CreateAssetMenu]
public class BurningRecipeSO : ScriptableObject {
    public KitchenObjectSO from;
    public KitchenObjectSO to;
    public float maximumProgress;
}
