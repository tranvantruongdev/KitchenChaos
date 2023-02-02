using UnityEngine;

public class ClearCounter : MonoBehaviour {
    [SerializeField] private GameObject counterVisual;
    [SerializeField] private KitchenObjectSO kitchenObj;
    [SerializeField] private Transform tomatoSpamPoint;

    private void Start() {
        Player.Instance.OnSelectCounterChanged += Player_OnSelectCounterChanged;
    }

    private void Player_OnSelectCounterChanged(object sender, Player.OnSelectCounterChangedArgs e) {
        if (e.selectedCounter == this) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        counterVisual.SetActive(true);
    }

    private void Hide() {
        counterVisual.SetActive(false);
    }

    public void Interact() {
        var obj = Instantiate(kitchenObj.prefab, tomatoSpamPoint);
        Debug.Log(obj.GetComponent<KitchenObject>().KitchenObjectSO.objName);
    }
}
