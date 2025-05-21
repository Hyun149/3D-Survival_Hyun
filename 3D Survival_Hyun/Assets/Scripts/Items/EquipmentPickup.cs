using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 맵 상의 장비 오브젝트가 플레이어에 의해 습득되어 장착되는 동작을 처리
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
            Debug.LogWarning("장비 장착 실패: 필드가 비어있습니다.");
            return;
        }

        if (!GoldSystem.Instance.SpendGold(goldCost))
        {
            Debug.Log("골드 부족: 장비를 장착할 수 없습니다.");
            return;
        }

        equipmentHandler.Equip(itemToEquip, targetSlotName);
        Debug.Log($"{goldCost}G를 소모하고 {itemToEquip.name} 장착 완료");
    }
}
