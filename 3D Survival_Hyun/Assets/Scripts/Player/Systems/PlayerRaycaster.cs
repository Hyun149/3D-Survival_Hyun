using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 카메라 시야 방향으로 Ray를 쏴서 상호작용 가능한 오브젝트를 감지하고,
/// 해당 정보를 UIManager를 통해 화면에 표시하거나 장비 장착 처리를 수행하는 컴포넌트
/// </summary>
public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cameraContainder;
    [SerializeField] private EquipmentManager equipmentManager;

    /// <summary>
    /// 매 프레임마다 카메라 전방으로 Ray를 발사하여
    /// 상호작용 가능한 오브젝트(Inspectable, EquipmentItem)을 탐색하고
    /// 해당 정보를 UI에 표시하거나 장비 장착 입력을 처리함
    /// </summary>
    private void Update()
    {
        Vector3 origin = cameraContainder.position;
        Vector3 direction = cameraContainder.forward;
        Debug.DrawRay(origin, direction * rayDistance, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, interactableLayer))
        {
            // 1. Inspectable 오브젝트인 경우 정보 출력
            var inspectable = hit.collider.GetComponent<InspectableObject>();
            if (inspectable != null && inspectable.data != null)
            {
                UIManager.Instance.ShowInspectInfo(inspectable.data.objectName, inspectable.data.description);
            }
            else
            {
                UIManager.Instance.HideInspectInfo();
            }

            // 2. 장비 아이템인 경우 장착 안내 및 입력 처리
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
