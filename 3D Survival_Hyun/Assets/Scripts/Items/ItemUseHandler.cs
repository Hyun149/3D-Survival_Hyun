using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public void UseItem(ItemData item)
    {
        switch (item.itemType)
        {
            case ItemType.Consumable:
                ApplyConsumableEffect(item);
                break;
            default:
                Debug.LogWarning($"[ItemUseHandler] 알 수 없는 아이템 타입: {item.itemType}");
                break;
        }
    }

    private void ApplyConsumableEffect(ItemData item)
    {
        if (item.itemName == "호박")
        {
            playerMovement.ApplyJumpBoost(20f, 5f);
            Debug.Log("호박을 먹었다! 일정시간동안 점프력이 상승합니다!");
        }
    }
}
