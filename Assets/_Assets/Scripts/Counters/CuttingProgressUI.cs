using UnityEngine;
using UnityEngine.UI;

public class CuttingProgressUI : MonoBehaviour {
    [SerializeField] private Image progress;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start() {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;

        progress.fillAmount = 0;

        Hide();
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.CuttingProgressArgs e) {
        if (e.progress <= 0) {
            Hide();
            return;
        }

        Show();
        progress.fillAmount = e.progress;
    }

    public void Show() {
        if (gameObject.activeInHierarchy) {
            return;
        }

        gameObject.SetActive(true);
    }

    public void Hide() {
        if (!gameObject.activeInHierarchy) {
            return;
        }

        gameObject.SetActive(false);
    }
}
