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
    /// 소비형 아이템 사용 효과 적용 
    /// enum 기반으로 효과 처리
    /// </summary>
    /// <param name="item"></param>
    private void ApplyConsumableEffect(ItemData item)
    {
        
        switch (item.effectType)
        {
            case ItemEffectType.JumpBoost:
                playerJumpHandler.ApplyJumpBoost(item.jumpBoostPower, item.jumpBoostDuration);
                Debug.Log($"{item.itemName} 사용: 점프력이 {item.jumpBoostPower}만큼 {item.jumpBoostDuration}초간 증가!");
                break;
            case ItemEffectType.Heal:
                var health = GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.Heal(item.healAmount);
                    Debug.Log($"{item.itemName} 사용: 체력 {item.healAmount} 회복!");
                }
                break;

            default:
                Debug.LogWarning($"[ItemUseHandler] 정의되지 않은 소비 아이템 효과: {item.effectType}");
                break;
        }
    }
}
