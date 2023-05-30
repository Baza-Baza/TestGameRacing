using UnityEngine;
using DG.Tweening;

public class UIScaler : MonoBehaviour
{
    [SerializeField]
    private RectTransform _bottomUIRect;
    [SerializeField]
    private RectTransform _wheelRect;

    public void SetSizeUI(GameModel gamemodel)
    {
        _bottomUIRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gamemodel.SizeScreenY / 7.3f);
    }

    public void RotateWheel(int direction)
    {
        _wheelRect.DORotate(new Vector3(1, 1, 15 * -direction), 0.2f).OnComplete(SetDefaultRotateWheel);
    }

    private void SetDefaultRotateWheel()
    {
        _wheelRect.DORotate(Vector3.one, 0.2f);
    }
}