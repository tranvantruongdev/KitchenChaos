using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] private GameObject counterVisual;
    [SerializeField] private BaseCounter baseCounter;

    private void Start() {
        Player.Instance.OnSelectCounterChanged += Player_OnSelectCounterChanged;
    }

    private void Player_OnSelectCounterChanged(object sender, Player.OnSelectCounterChangedArgs e) {
        if (e.selectedCounter == baseCounter) {
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
}
