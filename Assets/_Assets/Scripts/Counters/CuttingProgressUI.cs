using UnityEngine;
using UnityEngine.UI;

public class CuttingProgressUI : MonoBehaviour
{
    [SerializeField] private Image progress;
    [SerializeField] private GameObject counter;

    private IHasProgress _hasProgress;

    private void Awake()
    {
        if (!counter.TryGetComponent(out _hasProgress))
        {
            Debug.Log("Missing IHasProgress counter\nDisabling script...");
            enabled = false;
        }
    }

    private void Start()
    {
        _hasProgress.OnProgressChanged += CuttingCounter_OnCuttingProgressChanged;

        progress.fillAmount = 0;

        Hide();
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, IHasProgress.ProgressArgs e)
    {
        if (e.progress <= 0)
        {
            Hide();
            return;
        }

        Show();
        progress.fillAmount = e.progress;
    }

    public void Show()
    {
        if (gameObject.activeInHierarchy)
        {
            return;
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        gameObject.SetActive(false);
    }
}
