using UnityEngine;

public class CountDownUIHandler : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _txtCountDown;

    private void Update()
    {
        HideIfCountDownCompleted();

        _txtCountDown.text = Mathf.Ceil(GameManager.Instance.WaitToStartTimer).ToString();
    }

    private void HideIfCountDownCompleted()
    {
        if (GameManager.Instance.WaitToStartTimer < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
