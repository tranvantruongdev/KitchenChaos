using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    [SerializeField] private Transform _tfOrderHolder;
    [SerializeField] private OrderUI _orderTemplate;

    private void Start()
    {
        DeliveryManager.S_Instance.OnOderAdded += OnOrderListChanged;
        DeliveryManager.S_Instance.OnOderDeliver += OnOrderListChanged;
    }

    private void OnDestroy()
    {
        DeliveryManager.S_Instance.OnOderAdded -= OnOrderListChanged;
        DeliveryManager.S_Instance.OnOderDeliver -= OnOrderListChanged;
    }

    private void OnOrderListChanged(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        var lsOrder = DeliveryManager.S_Instance.LsDeliveryRecipeSO;
        _orderTemplate.SetOrder(lsOrder[0]);

        for (int i = 1; i < _tfOrderHolder.childCount; i++)
        {
            Destroy(_tfOrderHolder.GetChild(i).gameObject);
        }

        for (int i = 1; i < lsOrder.Count; i++)
        {
            var order = Instantiate(_orderTemplate, _tfOrderHolder);
            order.SetOrder(lsOrder[i]);
        }
    }
}
