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
    /// �Һ��� ������ ��� ȿ�� ���� (��: ������ ����)
    /// </summary>
    /// <param name="item"></param>
    private void ApplyConsumableEffect(ItemData item)
    {
        if (item.itemName == "ȣ��")
        {
            playerJumpHandler.ApplyJumpBoost(15f, 5f);
            Debug.Log("ȣ���� �Ծ���! �����ð����� �������� ����մϴ�!");
        }

        if (item.itemName == "����")
        {
            var health = GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(item.healAmount);
                Debug.Log("��� �Ծ���! ü�� ȸ��!");
            }
        }
    }
}
