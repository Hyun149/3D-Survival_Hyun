using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �� ���� ��� ������Ʈ�� �÷��̾ ���� ����Ǿ� �����Ǵ� ������ ó��
/// </summary>
public class EquipmentPickup : MonoBehaviour
{
    [SerializeField] private EquipmentHandler equipmentHandler;
    [SerializeField] private GameObject itemToEquip;
    [SerializeField] private string targetSlotName;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int goldCost = 100;

    private InputAction equipAction;

    private void OnEnable()
    {
        if (playerInput != null)
        {
            equipAction = playerInput.actions["Equip"];
            equipAction.performed += OnEquipPerformed;
            equipAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (equipAction != null)
        {
            equipAction.performed -= OnEquipPerformed;
            equipAction.Disable();
        }
    }

    private void OnEquipPerformed(InputAction.CallbackContext context)
    {
        if (equipmentHandler == null || itemToEquip == null)
        {
            Debug.LogWarning("��� ���� ����: �ʵ尡 ����ֽ��ϴ�.");
            return;
        }

        if (!GoldSystem.Instance.SpendGold(goldCost))
        {
            Debug.Log("��� ����: ��� ������ �� �����ϴ�.");
            return;
        }

        equipmentHandler.Equip(itemToEquip, targetSlotName);
        Debug.Log($"{goldCost}G�� �Ҹ��ϰ� {itemToEquip.name} ���� �Ϸ�");
    }
}
