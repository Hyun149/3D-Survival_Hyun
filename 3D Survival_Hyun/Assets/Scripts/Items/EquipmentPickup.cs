using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 맵 상에 존재하는 장비 아이템을 플레이어가 습득하여 장착하는 동작을 처리하는 클래스입니다.
/// 골드를 지불하고 특정 슬롯에 장비를 장착하며, 장착 완료 후 해당 오브젝트는 사라집니다.
/// </summary>
public class EquipmentPickup : MonoBehaviour
{
    [SerializeField] private EquipmentHandler equipmentHandler;
    [SerializeField] private GameObject itemToEquip;
    [SerializeField] private string targetSlotName;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int goldCost;

    private InputAction equipAction;

    /// <summary>
    /// 입력 액션 맵에서 'Equip' 액션을 찾습니다.
    /// </summary>
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
    /// 장비 장착 입력이 발생했을 때 호출되는 메서드입니다.
    /// 골드가 충분할 경우 장비를 장착하고, 오브젝트를 삭제합니다.
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
            return;
        }

        equipmentHandler.Equip(itemToEquip, targetSlotName);

        Destroy(transform.root.gameObject);
    }
}
