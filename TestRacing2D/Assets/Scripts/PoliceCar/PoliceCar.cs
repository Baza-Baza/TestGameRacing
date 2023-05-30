using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class PoliceCar : MonoBehaviour
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

    private float _startPosY;

    private bool _canMoveUp;

    public Action PoliceCarCrashed
    {
        get;
        set;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            PoliceCarCrashed.Invoke();
        }
    }

    public void SetData(float posX, int sizeScreenY)
    {
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeScreenY / 6);
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeScreenY / 10);

        _colider.size = new Vector2(sizeScreenY / 10, sizeScreenY / 6);

        _rect.anchoredPosition = new Vector2(posX, -sizeScreenY / 2 - _rect.rect.width / 2);
        _startPosY = -_rect.anchoredPosition.y;

        _canMoveUp = true;
    }

    public void MoveToStartPos()
    {
        MovePoliceCarY((int)_startPosY);
    }

    public void MovePoliceCarX(int direction, bool isMove)
    {
        StartCoroutine(Delay(direction, isMove));
    }

    public void MovePoliceCarY(int target)
    {
        if (_canMoveUp)
        {
            _rect.transform.DOLocalMoveY(-target, 1.5f).OnComplete(DestroyCar);
        }
    }

    private IEnumerator Delay(int direction, bool isMove)
    {
        yield return new WaitForSeconds(0.3f);

        float x = _rect.anchoredPosition.x + direction * _speed;

        if (isMove)
        {
            _rect.transform.DOLocalRotate(new Vector3(0, 0, -10 * direction), 0);
            _rect.transform.DOLocalMoveX(CheckMaxAndMinPos(x), 0.2f).SetEase(Ease.Flash).OnComplete(RotatePoliceCarToDefault);
        }
    }

    private void RotatePoliceCarToDefault()
    {
        _rect.transform.DOLocalRotate(Vector3.one, 0);
    }

    private float CheckMaxAndMinPos(float x)
    {
        float newX = Mathf.Clamp(x, _minX, _maxX);

        return newX;
    }

    private void DestroyCar()
    {
        if (-_rect.transform.localPosition.y == _startPosY)
        {
            _canMoveUp = false;
            StartCoroutine(DelayDestroy());
        }
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        PoliceCarCrashed.Invoke();
    }
}