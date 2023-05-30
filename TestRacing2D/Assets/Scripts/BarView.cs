using UnityEngine;

public class BarView : MonoBehaviour
{
    [SerializeField]
    private Bar _healthBar;
    [SerializeField]
    private Bar _magnetBar;
    [SerializeField]
    private Bar _shieldBar;
    [SerializeField]
    private Bar _nitroBar;

    public void UpdateHealthBar(float value)
    {
        _healthBar.ChangeFillImage(value);
    }

    public void ActiveMagnetBar(bool active)
    {
        if (active)
        {
            _magnetBar.ChangeFillImage(-1);
        }

        _magnetBar.gameObject.SetActive(active);
    }

    public void ActiveShieldBar(bool active)
    {
        if (active)
        {
            _shieldBar.ChangeFillImage(-1);
        }

        _shieldBar.gameObject.SetActive(active);
    }

    public void ActiveNitroBar(bool active)
    {
        if (active)
        {
            _nitroBar.ChangeFillImage(-1);
        }

        _nitroBar.gameObject.SetActive(active);
    }

    public void UpdateMagnetBar()
    {
        _magnetBar.ChangeFillImage(0.066f);
    }

    public void UpdateShieldtBar()
    {
        _shieldBar.ChangeFillImage(0.066f);
    }

    public void UpdateShieldBar()
    {
        _nitroBar.ChangeFillImage(0.066f);
    }
}