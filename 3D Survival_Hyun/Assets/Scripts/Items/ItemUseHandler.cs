using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 사용 효과를 처리하는 클래스
/// 아이템 타입에 따라 적절한 행동을 수행함
/// </summary>
public class ItemUseHandler : MonoBehaviour
{
    [SerializeField] private PlayerJumpHandler playerJumpHandler;

    private void Awake()
    {
        if (playerJumpHandler == null)
            playerJumpHandler = FindObjectOfType<PlayerJumpHandler>();
    }

    /// <summary>
    /// 전달받은 아이템을 타입에 따라 사용 처리
    /// </summary>
    /// <param name="item"></param>
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

    /// <summary>
    /// 소비형 아이템 사용 효과 적용 (예: 점프력 증가)
    /// </summary>
    /// <param name="item"></param>
    private void ApplyConsumableEffect(ItemData item)
    {
        if (item.itemName == "호박")
        {
            playerJumpHandler.ApplyJumpBoost(15f, 5f);
            Debug.Log("호박을 먹었다! 일정시간동안 점프력이 상승합니다!");
        }

        if (item.itemName == "연어")
        {
            var health = GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(item.healAmount);
                Debug.Log("연어를 먹었다! 체력 회복!");
            }
        }
    }
}
