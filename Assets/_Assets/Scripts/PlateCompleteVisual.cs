using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [System.Serializable]
    public struct KitchenObjectData
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject kitchenObjectPrefab;
    }

    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private List<KitchenObjectData> kitchenObjects = new();

    // Start is called before the first frame update
    private void Start()
    {
        plate.OnIngredientAdded += Plate_OnIngridentAdded;
    }

    private void Plate_OnIngridentAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs e)
    {
        foreach (var obj in kitchenObjects)
        {
            if (obj.kitchenObjectSO == e.KitchenObjectSO)
            {
                if (obj.kitchenObjectPrefab.activeInHierarchy)
                {
                    // Dont reactive the object
                    return;
                }

                obj.kitchenObjectPrefab.SetActive(true);
            }
        }
    }
}
