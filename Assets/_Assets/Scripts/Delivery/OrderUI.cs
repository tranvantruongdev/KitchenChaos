using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _txtOrderName;
    [SerializeField] private Transform _tfHolder;
    [SerializeField] private Image _imgOderIcon;

    public void SetOrder(DeliveryRecipeSO deliveryRecipeSO)
    {
        _txtOrderName.text = deliveryRecipeSO.Name;
        HandleIngredientIcons(deliveryRecipeSO);
    }

    private void HandleIngredientIcons(DeliveryRecipeSO deliveryRecipeSO)
    {
        List<KitchenObjectSO> lsIngredient = deliveryRecipeSO.LsIngredient;
        _imgOderIcon.sprite = lsIngredient[0].sprite;

        for (int i = 1; i < _tfHolder.childCount; i++)
        {
            Destroy(_tfHolder.GetChild(i).gameObject);
        }

        for (int i = 1; i < lsIngredient.Count; i++)
        {
            var icon = Instantiate(_imgOderIcon, _tfHolder);
            icon.sprite = lsIngredient[i].sprite;
        }
    }
}
