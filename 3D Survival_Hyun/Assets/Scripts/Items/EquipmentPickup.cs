using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �� �� �����ϴ� ��� �������� �÷��̾ �����Ͽ� �����ϴ� ������ ó���ϴ� Ŭ�����Դϴ�.
/// ��带 �����ϰ� Ư�� ���Կ� ��� �����ϸ�, ���� �Ϸ� �� �ش� ������Ʈ�� ������ϴ�.
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
    /// �Է� �׼� �ʿ��� 'Equip' �׼��� ã���ϴ�.
    /// </summary>
    private void Awake()
    {
        if (playerInput != null)
        {
            equipAction = playerInput.actions.FindAction("Equip", true);
        }
    }

    /// <summary>
    /// �Է� �ý��� Ȱ��ȭ �� ��� ���� Ű�� ����
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
    /// ��Ȱ��ȭ �� �Է� �׼� ���� ����
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
    /// ��� ���� �Է��� �߻����� �� ȣ��Ǵ� �޼����Դϴ�.
    /// ��尡 ����� ��� ��� �����ϰ�, ������Ʈ�� �����մϴ�.
    /// </summary>
    /// <param name="context"></param>
    private void OnEquipPerformed(InputAction.CallbackContext context)
    {
        if (equipmentHandler == null || itemToEquip == null)
        {
            Debug.LogWarning("��� ���� ����: �ʵ尡 ����ֽ��ϴ�.");
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
