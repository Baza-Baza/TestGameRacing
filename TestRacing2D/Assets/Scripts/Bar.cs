using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private bool _isHealth;

    public void ChangeFillImage(float value)
    {
        if (_isHealth)
        {
            _image.fillAmount = value;
        }
        else
        {
            _image.fillAmount -= value;
        }
    }
}
