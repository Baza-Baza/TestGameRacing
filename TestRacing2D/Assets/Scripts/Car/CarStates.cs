using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarStates : MonoBehaviour
{
    [SerializeField]
    private List<Image> _stateImages;
    [SerializeField]
    private Color _lightMagnetColor;
    [SerializeField]
    private Color _lightNitroColor;
    [SerializeField]
    private Color _lightShieldColor;

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Default:
                SetDefaultState();
                break;
            case State.Magnet:
                SetMagnetState();
                break;
            case State.Nitro:
                SetNitroState();
                break;
            case State.Shield:
                SetShieldState();
                break;
        }
    }

    private void SetDefaultState()
    {
        foreach (Image state in _stateImages)
        {
            state.gameObject.SetActive(false);
        }
    }

    private void SetMagnetState()
    {
        SetDefaultState();

        _stateImages[0].gameObject.SetActive(true);
        _stateImages[0].color = _lightMagnetColor;
        _stateImages[1].gameObject.SetActive(true);
    }

    private void SetNitroState()
    {
        SetDefaultState();

        _stateImages[0].gameObject.SetActive(true);
        _stateImages[0].color = _lightNitroColor;
        _stateImages[2].gameObject.SetActive(true);
    }

    private void SetShieldState()
    {
        SetDefaultState();

        _stateImages[0].gameObject.SetActive(true);
        _stateImages[0].color = _lightShieldColor;
        _stateImages[3].gameObject.SetActive(true);
    }
}
