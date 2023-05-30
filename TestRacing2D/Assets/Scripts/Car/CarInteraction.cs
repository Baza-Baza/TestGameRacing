using System;
using UnityEngine;

public class CarInteraction : MonoBehaviour
{
    public Action<TriggersEnum> TriggerWithObject
    {
        get;
        set;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject);
    }

    private void CheckCollision(GameObject go)
    {
        switch (go.tag)
        {
            case "Heart":
                Destroy(go);
                Notification(TriggersEnum.Heart);
                break;
            case "Coin":
                Destroy(go);
                Notification(TriggersEnum.Coin);
                break;
            case "Block":
                Notification(TriggersEnum.Block);
                break;
            case "Crack":
                Notification(TriggersEnum.Crack);
                break;
            case "Oil":
                Notification(TriggersEnum.Oil);
                break;
            case "Magnet":
                Destroy(go);
                Notification(TriggersEnum.Magnet);
                break;
            case "Shield":
                Destroy(go);
                Notification(TriggersEnum.Shield);
                break;
            case "Nitro":
                Destroy(go);
                Notification(TriggersEnum.Nitro);
                break;
            case "Police":
                Notification(TriggersEnum.Police);
                break;
        }
    }

    private void DestroyTriggerObject(GameObject go)
    {
        Destroy(go);
    }

    private void Notification(TriggersEnum triggerEnum)
    {
        TriggerWithObject.Invoke(triggerEnum);
    }
}