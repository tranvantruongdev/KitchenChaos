using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingle : MonoBehaviour
{
    [SerializeField] private Image _imgIcon;

    public void SetIcon(Sprite icon)
    {
        _imgIcon.sprite = icon;
    }
}
