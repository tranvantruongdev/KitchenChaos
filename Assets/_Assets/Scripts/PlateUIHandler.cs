using System.Collections.Generic;
using UnityEngine;

public class PlateUIHandler : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private GameObject _goIconTemplate;
    [SerializeField] private PlateIconSingle _templatePlateIconSingle;

    private readonly List<KitchenObjectSO> _lsKitchenObjectSO = new();

    private void Start()
    {
        _plateKitchenObject.OnIngridentAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void OnDestroy()
    {
        _plateKitchenObject.OnIngridentAdded -= PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngridentAddedArgs e)
    {
        UpdateUI(e.KitchenObjectSO);
    }

    private void UpdateUI(KitchenObjectSO kitchenObjectSO)
    {
        // Remove all the child except the template one
        for (int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        // Add to object list
        _lsKitchenObjectSO.Add(kitchenObjectSO);
        // Update template icon
        _templatePlateIconSingle.SetIcon(_lsKitchenObjectSO[0].sprite);
        _goIconTemplate.SetActive(true);
        // Update remaining icons
        for (int i = 1; i < _lsKitchenObjectSO.Count; i++)
        {
            GameObject goTemplate = Instantiate(_goIconTemplate, transform);
            goTemplate.GetComponent<PlateIconSingle>().SetIcon(_lsKitchenObjectSO[i].sprite);
            goTemplate.SetActive(true);
        }
    }
}
