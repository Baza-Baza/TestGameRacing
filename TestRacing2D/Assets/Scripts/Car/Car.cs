using System;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rect;
    [SerializeField]
    private BoxCollider2D _colider;
    [SerializeField]
    private int _speed;
    [SerializeField]
    private int _maxX;
    [SerializeField]
    private int _minX;

    public Action ReachedStartPos
    {
        get;
        set;
    }

    public float PosX
    {
        get
        {
            return _rect.anchoredPosition.x;
        }
    }

    public void SetData(int screenSizeX, int screenSizeY)
    {
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, screenSizeX / 5f);
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, screenSizeY / 8f);
        _colider.size = new Vector2(screenSizeX / 5f, screenSizeY / 8f);

        _rect.anchoredPosition = new Vector2(screenSizeX / 7, -screenSizeY / 3.4f);
    }

    public void SetStartStartPosition()
    {
        _rect.transform.DOLocalMoveY(-50, 1.5f).OnComplete(NotificattionReachedStartPos);
    }

    public void MoveCarX(int direction, bool isMove)
    {
        float x = _rect.anchoredPosition.x + direction * _speed;

        if (isMove)
        {
            Debug.Log(CheckMaxAndMinPos(x));

            _rect.transform.DOLocalRotate(new Vector3(0, 0, -10 * direction), 0);
            _rect.transform.DOLocalMoveX(CheckMaxAndMinPos(x), 0.2f).SetEase(Ease.Flash).OnComplete(RotateCarToDefault);
        }
    }

    private float CheckMaxAndMinPos(float x)
    {
        float newX = Mathf.Clamp(x, _minX, _maxX);

        return newX;
    }

    private void RotateCarToDefault()
    {
        _rect.transform.DOLocalRotate(Vector3.one, 0);
    }

    private void NotificattionReachedStartPos()
    {
        ReachedStartPos.Invoke();
    }
}