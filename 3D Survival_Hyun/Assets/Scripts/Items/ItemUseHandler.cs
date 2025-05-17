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
                Debug.LogWarning($"[ItemUseHandler] �� �� ���� ������ Ÿ��: {item.itemType}");
                break;
        }
    }

    private void ApplyConsumableEffect(ItemData item)
    {
        if (item.itemName == "ȣ��")
        {
            playerMovement.ApplyJumpBoost(20f, 5f);
            Debug.Log("ȣ���� �Ծ���! �����ð����� �������� ����մϴ�!");
        }
    }
}
