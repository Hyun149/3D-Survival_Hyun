using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 맵 상에 존재하는 장비 아이템을 플레이어가 습득하여 장착하는 동작을 처리하는 클래스
/// 골드를 지불하여 장비를 특정 슬롯에 장착함
/// </summary>
public class EquipmentPickup : MonoBehaviour
{
    [SerializeField] private EquipmentHandler equipmentHandler;
    [SerializeField] private GameObject itemToEquip;
    [SerializeField] private string targetSlotName;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int goldCost;

    private InputAction equipAction;

    private void Awake()
    {
        if (playerInput != null)
        {
            equipAction = playerInput.actions.FindAction("Equip", true);
        }
    }

    /// <summary>
    /// 입력 시스템 활성화 시 장비 장착 키를 구독
    /// </summary>
    private void OnEnable()
    {
        if (equipAction != null)
        {
            equipAction.performed += OnEquipPerformed;
            equipAction.Enable();
        }
    }

    /// <summary>
    /// 비활성화 시 입력 액션 연결 해제
    /// </summary>
    private void OnDisable()
    {
        if (equipAction != null)
        {
            equipAction.performed -= OnEquipPerformed;
            equipAction.Disable();
        }
    }

    /// <summary>
    /// 장비 장착 입력이 발생했을 때 실행됨
    /// 골드가 충분하면 장비를 장착하고, 그렇지 않으면 실패 로그 출력
    /// </summary>
    /// <param name="context"></param>
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
