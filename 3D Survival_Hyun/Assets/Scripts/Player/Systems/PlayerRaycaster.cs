using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ī�޶� �þ� �������� Ray�� ���� ��ȣ�ۿ� ������ ������Ʈ�� �����ϰ�,
/// �ش� ������ UIManager�� ���� ȭ�鿡 ǥ���ϰų� ��� ���� ó���� �����ϴ� ������Ʈ
/// </summary>
public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cameraContainder;
    [SerializeField] private EquipmentManager equipmentManager;

    /// <summary>
    /// �� �����Ӹ��� ī�޶� �������� Ray�� �߻��Ͽ�
    /// ��ȣ�ۿ� ������ ������Ʈ(Inspectable, EquipmentItem)�� Ž���ϰ�
    /// �ش� ������ UI�� ǥ���ϰų� ��� ���� �Է��� ó����
    /// </summary>
    private void Update()
    {
        Vector3 origin = cameraContainder.position;
        Vector3 direction = cameraContainder.forward;
        Debug.DrawRay(origin, direction * rayDistance, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, interactableLayer))
        {
            // 1. Inspectable ������Ʈ�� ��� ���� ���
            var inspectable = hit.collider.GetComponent<InspectableObject>();
            if (inspectable != null && inspectable.data != null)
            {
                UIManager.Instance.ShowInspectInfo(inspectable.data.objectName, inspectable.data.description);
            }
            else
            {
                UIManager.Instance.HideInspectInfo();
            }

            // 2. ��� �������� ��� ���� �ȳ� �� �Է� ó��
            if (hit.collider.TryGetComponent<EquipmentItem>(out EquipmentItem item))
            {
                UIManager.Instance.ShowEquipPrompt(item.itemName);

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    equipmentManager.Equip(item);
                }
            }
            else
            {
                UIManager.Instance.HideEquipPrompt();
            }
        }
        else
        {
            UIManager.Instance.HideInspectInfo();
            UIManager.Instance.HideEquipPrompt();
        }
    }
}
