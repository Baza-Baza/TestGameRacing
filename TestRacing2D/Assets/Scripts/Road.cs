using System;
using UnityEngine;
using DG.Tweening;

public class Road : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rect;
    [SerializeField]
    private int _speedMove;

    private bool _isMove;
    private int _targetPos;

    public Action TargetPos
    {
        get;
        set;
    }

    public float Pos
    {
        get
        {
            return _rect.transform.localPosition.y;
        }
    }

    public int SpeedMove
    {
        get
        {
            return _speedMove;
        }
        set
        {
            _speedMove = value;
        }
    }

    private void Update()
    {
        if (_isMove)
        {
            _rect.transform.position -= new Vector3(0, _speedMove * Time.deltaTime, 0);
        }

        if (_rect.transform.localPosition.y < _targetPos)
        {
            Notification();
        }
    }

    public void SetData(float positionY, int sizeX, int sizeY)
    {
        _targetPos = -sizeY;

        _rect.anchoredPosition = new Vector2(0, positionY);
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeX);
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeY);
    }

    public void SetMoveRoad(bool isMove)
    {
        _isMove = isMove;
    }

    private void Notification()
    {
        TargetPos.Invoke();
    }
}