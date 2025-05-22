using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��� ȿ���� ó���ϴ� Ŭ����
/// ������ Ÿ�Կ� ���� ������ �ൿ�� ������
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
    /// ���޹��� �������� Ÿ�Կ� ���� ��� ó��
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
                Debug.LogWarning($"[ItemUseHandler] �� �� ���� ������ Ÿ��: {item.itemType}");
                break;
        }
    }

    /// <summary>
    /// �Һ��� ������ ��� ȿ�� ���� 
    /// enum ������� ȿ�� ó��
    /// </summary>
    /// <param name="item"></param>
    private void ApplyConsumableEffect(ItemData item)
    {
        
        switch (item.effectType)
        {
            case ItemEffectType.JumpBoost:
                playerJumpHandler.ApplyJumpBoost(item.jumpBoostPower, item.jumpBoostDuration);
                Debug.Log($"{item.itemName} ���: �������� {item.jumpBoostPower}��ŭ {item.jumpBoostDuration}�ʰ� ����!");
                break;
            case ItemEffectType.Heal:
                var health = GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.Heal(item.healAmount);
                    Debug.Log($"{item.itemName} ���: ü�� {item.healAmount} ȸ��!");
                }
                break;

            default:
                Debug.LogWarning($"[ItemUseHandler] ���ǵ��� ���� �Һ� ������ ȿ��: {item.effectType}");
                break;
        }
    }
}
