using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    private Image _imageNumbers;
    [SerializeField]
    private Image _startImage;
    [SerializeField]
    private List<Sprite> _sprites;

    public void SetImage(int value)
    {
        if (value == 0)
        {
            _imageNumbers.gameObject.SetActive(false);
            _startImage.gameObject.SetActive(true);
        }
        else if (value > 0)
        {
            _imageNumbers.sprite = _sprites[value-1];
        }
        else if (value == -1)
        {
            _startImage.gameObject.SetActive(false);
        }
    }
}
