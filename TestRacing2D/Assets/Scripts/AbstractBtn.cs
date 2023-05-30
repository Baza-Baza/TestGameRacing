using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbstractBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Button _btn;

    public Action<bool> PressButton
    {
        get;
        set;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_btn.interactable)
        {
            PressButton.Invoke(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_btn.interactable)
        {
            PressButton.Invoke(false);
        }
    }

    public void ChangeInteractble(bool value)
    {
        _btn.interactable = value;
    }
}