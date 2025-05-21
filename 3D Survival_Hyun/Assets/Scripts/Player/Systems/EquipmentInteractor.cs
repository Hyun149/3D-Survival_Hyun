using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어가 조사 중인 아이템을 E키로 장착하는 인터렉션 시스템
/// </summary>
public class EquipmentInteractor : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask equipmentLayer;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private EquipmentManager equipmentManager;

    private void Update()
    {
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactDistance, equipmentLayer))
        {
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
            UIManager.Instance.HideEquipPrompt();
        }
    }
}
