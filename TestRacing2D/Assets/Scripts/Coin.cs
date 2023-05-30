using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MagnetCar")
        {
            MoveToMagnet(collision.gameObject.transform.position);
        }
    }

    private void MoveToMagnet(Vector2 targetPos)
    {
        transform.DOMove(targetPos, 0.4f).SetEase(Ease.Flash);
    }
}
